using System;
using System.Collections.Generic;
using System.Data;
using BecaDotNet.Domain.Model;
using BecaDotNet.Domain.Repository;
using BecaDotNet.Infrastructure;

namespace BecaDotNet.Repository
{
    public class UserRepository : IRepository<User>, IUserRepository
    {
        public User Create(User newEntity)
        {
            if (!CanCreateUser(newEntity))
                return new User();

            using (var factory = new ConnectionFactory())
            {
                var cmdText = "STP_Register_User";
                var parametros = new Dictionary<string, object>
                {
                     {"@NAME"     , newEntity.Name}
                    ,{"@EMAIL"    , newEntity.Email}
                    ,{"@LOGIN"    , newEntity.Login}
                    ,{"@PASSWORD" , newEntity.Password}
                };
                var result = factory.ExecuteScalar(cmdText, CommandType.StoredProcedure, parametros);
                int.TryParse(result.ToString(), out int newUserId);

                if (newUserId == 0)
                    throw new Exception("Erro ao gravar usuário no banco");
                return GetSingle(newUserId);
            }
        }

        private bool CanCreateUser(User newUser)
        {
            return newUser != null && (
            !string.IsNullOrEmpty(newUser.Name) &&
            !string.IsNullOrEmpty(newUser.Login) &&
            !string.IsNullOrEmpty(newUser.Password) &&
            !string.IsNullOrEmpty(newUser.Email)
            );
        }

        public IEnumerable<User> GetMany(User filter)
        {
            try
            {
                using (var factory = new ConnectionFactory())
                {
                    var parametros = new Dictionary<string, object>();
                    if (filter.UserTypeId > 0)
                        parametros.Add("@userTypeId", filter.UserTypeId);
                    if (filter.SuperiorId.HasValue && filter.SuperiorId > 0)
                        parametros.Add("@superiorId", filter.SuperiorId);
                    if (!string.IsNullOrEmpty(filter.Name))
                        parametros.Add("@userName", filter.Name);
                    var sqlText = "STP_GET_USER";
                    return GetManyEntityFromReader(factory.GetReader(sqlText, CommandType.StoredProcedure, parametros));
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public IEnumerable<User> GetManyEntityFromReader(IDataReader reader)
        {
            var result = new List<User>();
            while (reader.Read())
            {
                var entityUser = GetEntityFromReader(reader);
                if (entityUser.Id > 0)
                    result.Add(entityUser);
            }
            return result;
        }

        public User GetEntityFromReader(IDataReader reader)
        {
            try
            {
                int.TryParse(reader["ID"].ToString(), out int userId);
                int.TryParse(reader["USER_TYPE_ID"].ToString(), out int userTypeId);
                int.TryParse(reader["SUPERIOR_ID"].ToString(), out int superiorId);
                DateTime.TryParse(reader["REGISTER_DATE"].ToString(), out DateTime registerDate);
                bool.TryParse(reader["IS_ACTIVE"].ToString(), out bool isActive);
                return new User
                {
                    Id = userId,
                    UserTypeId = userTypeId,
                    RegisterDate = registerDate,
                    IsActive = isActive,
                    SuperiorId = superiorId == 0 ? null : (int?)superiorId,
                    Email = reader["EMAIL"].ToString(),
                    Login = reader["LOGIN"].ToString(),
                    Name = reader["FULL_NAME"].ToString(),
                    UserType = new UserType
                    {
                        Description = reader["DESC_USER_TYPE"].ToString(),
                        Id = userTypeId
                    }
                };
            }
            catch (Exception ex)
            {
                return new User();
            }
        }

        public User GetSingle(int userId)
        {
            var usuarioFromReader = new User();
            var sqlText = "STP_GET_USER";
            var parametros = new Dictionary<string, object>() {
                { "@user_id",userId }
            };
            using (var factory = new ConnectionFactory())
            {
                var readerDaFactory = factory.GetReader(sqlText, CommandType.StoredProcedure, parametros);
                if (readerDaFactory.Read())
                    usuarioFromReader = GetEntityFromReader(readerDaFactory);
            }
            return usuarioFromReader;
        }

        public bool Remove(int entityId)
        {
            if (entityId == 0)
                return false;

            using (var factory = new ConnectionFactory())
            {
                var cmdText = "update tb_user set [is_active]=0 where [id]=@UserId";
                var parametros = new Dictionary<string, object>
                {
                    {"@UserId"    , entityId}
                };
                var result = factory.ExecuteNonQuery(cmdText, CommandType.Text, parametros);
                return true;
            }
        }

        public User Update(User updatedEntity)
        {
            if (string.IsNullOrEmpty(updatedEntity.Name))
                return null;

            using (var factory = new ConnectionFactory())
            {
                var cmdText = "update tb_user set [name]=@name where [id]=@UserId";
                var parametros = new Dictionary<string, object>
                {
                     {"@name"     , updatedEntity.Name}
                    ,{"@UserId"    , updatedEntity.Id}
                };
                var result = factory.ExecuteNonQuery(cmdText, CommandType.Text, parametros);
                return GetSingle(updatedEntity.Id);
            }
        }

        public User Authenticate(string login, string password)
        {
            var userId = GetUserId(login, password);
            return userId > 0 ? GetSingle(userId) : new User();
        }

        private int GetUserId(string login, string password)
        {
            using (var factory = new ConnectionFactory())
            {
                var sqlText = "select id from tb_user where [login]=@login and [password]=@password";
                var parametros = new Dictionary<string, object>
                {
                    {"@login",login },
                    {"@password",password }
                };
                var result = factory.ExecuteScalar(sqlText, CommandType.Text, parametros);
                int.TryParse(result.ToString(), out int userId);
                return userId;
            }
        }
    }
}
