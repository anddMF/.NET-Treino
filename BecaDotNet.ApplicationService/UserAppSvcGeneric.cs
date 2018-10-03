using System;
using System.Collections.Generic;
using System.Linq;
using BecaDotNet.Domain.Model;
using BecaDotNet.Domain.Service;
using BecaDotNet.Repository;
using NLog;

namespace BecaDotNet.ApplicationService
{
    public class UserAppSvcGeneric : IGenericService<User>
    {
        private UserRepositoryGeneric rep = new UserRepositoryGeneric();
        //private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private void CreateUserTypeUser(User user)
        {
            var userTypeUser = new UserTypeUserAppSvcGeneric().
                Create(new UserTypeUser
                {
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    StartDate = DateTime.Now,
                    UserId = user.Id,
                    UserTypeId = user.UserTypeId
                });
        }



        public User Create(User toCreate)
        {
            try
            {
                toCreate.IsActive = true;
                rep.Create(toCreate);
                rep.Save();
                CreateUserTypeUser(toCreate);
                return toCreate;
            }
            catch (Exception wz)
            {
                return null;
            }
        }

        public bool Delete(int id)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
            try
            {
                rep.Delete(id);
                rep.Save();
                
                logger.Info("Info do DELETE");
                logger.Log(LogLevel.Warn, "WARN vindo do .Log");
                return true;
            }catch(Exception e)
            {
                logger.Error(e, "ERROR");
                return false;
            }
        }

        public IEnumerable<User> FindBy(User filter)
        {
            if (filter == null)
                filter = new User();
            var logger = NLog.LogManager.GetCurrentClassLogger();
            try
            {
                var result = rep.FindBy(item => item.Name.Contains(string.IsNullOrEmpty(filter.Name) ? item.Name : filter.Name), a => a.UserType, a => a.Superior
                    ).ToList();

                logger.Info("Info do FIND");
                logger.Log(LogLevel.Warn, "WARN vindo do .Log");
                logger.Error("ERROR FIND");
                return result;
            }
            catch (Exception e)
            {
                logger.Error(e, "ERROR FIND");
                return new List<User>();
            }
        }

        public User Get(int id)
        {
            try
            {
                return rep.GetSingle(id);
            }
            catch
            {
                return null;
            }
        }

        public User Update(User toUpdate)
        {
            try
            {
                var bdUser = Get(toUpdate.Id);
                if (bdUser.Id == toUpdate.Id)
                {
                    bdUser.Name = toUpdate.Name;
                    bdUser.Login = toUpdate.Login;
                    bdUser.Password = toUpdate.Password;
                    bdUser.ProjetoAtualId = toUpdate.ProjetoAtualId;
                    bdUser.UserTypeId = toUpdate.UserTypeId;
                    rep.Update(bdUser);
                }
                else
                    return new User();

                rep.Save();
                return toUpdate;
            }catch(Exception e)
            {
                return new User();
            }
        }

        public User Authenticate(string login, string password)
        {
            return rep.Authenticate(login, password);
        }
    }
}
