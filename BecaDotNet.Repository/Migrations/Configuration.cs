namespace BecaDotNet.Repository.Migrations
{
    using BecaDotNet.Domain.Model;
    using BecaDotNet.Repository.Context;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<BecaContext>
    {
        #region RemoveProceIfExists
        private const string RemoveProceIfExists = @"
if(object_id('STP_Register_User') is not null)
    drop procedure STP_Register_User

if(object_id('STP_GET_USER') is not null)
    drop procedure STP_GET_USER
";
        #endregion RemoveProceIfExists

        #region STP_GET_USER
        private const string STP_GET_USER = @"
CREATE procedure STP_GET_USER(      
 @nome varchar(100) = null,      
 @login varchar(100) = null,      
 @register_date datetime= null,      
 @user_type_id int = null,      
 @superior_id int = null,    
 @user_id int = null    
)      
as      
begin      
 select u.*,ut.DESC_USER_TYPE      
   from tb_user              u      
   join TB_USER_TYPE_USER    utu on utu.user_id             = u.id       
           and utu.user_type_id        = u.user_type_id      
           and utu.is_active             = 1       
   join TB_USER_TYPE         ut  on ut.ID                  = utu.user_type_id      
  where u.IS_ACTIVE = 1  
    and u.FULL_NAME like case when @nome is null then u.FULL_NAME else '%'+@nome+'%'end      
    and u.LOGIN like case when @login is null then u.login else '%'+@login+'%'end      
    and cast(u.REGISTER_DATE as date) = cast(isnull(@register_date, u.REGISTER_DATE) as date)      
    and u.user_type_id = isnull(@user_type_id,u.user_type_id)      
    and isnull(u.superior_id,-1) = isnull(@superior_id , isnull(u.superior_id,-1))    
 and u.ID = isnull(@user_id,u.ID)      
end";
        #endregion STP_GET_USER

        #region STP_REGISTER_USER
        private const string STP_REGISTER_USER = @"
CREATE procedure STP_Register_User(    
       @NAME     varchar(200),    
       @EMAIL   varchar(200),    
       @LOGIN   varchar(100),    
       @PASSWORD varchar(150)    
)    
as    
begin    
   declare @UserId int 
 insert into tb_user    
  (    
 [FULL_NAME],    
 EMAIL,    
 [LOGIN],    
 [PASSWORD],
 [REGISTER_DATE],
 [IS_ACTIVE],
[USER_TYPE_ID]
 )    
 values    
 (@name,@email,@login,@password,getdate(),1,2)    
    
  set @UserId  = @@IDENTITY;  
  insert into TB_USER_TYPE_USER values(@UserId,2,getdate(),getdate(),null,1)   
  select @UserId as UserId  
end";
        #endregion STP_REGISTER_USER

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BecaContext context)
        {

            context.UserTypes.AddOrUpdate(new UserType { Id = 1, Description = "Admin", IsActive = true });
            context.UserTypes.AddOrUpdate(new UserType { Id = 2, Description = "Padrão", IsActive = true });
            context.Users.AddOrUpdate(new User { Id = 1,
                Email = "admin@mail.com",
                IsActive = true,
                Login = "Admin",
                Name = "User Admin",
                Password="pwd123",
                RegisterDate=DateTime.Now,
                UserTypeId =1
            });
            context.Users.AddOrUpdate(new User
            {
                Id = 2,
                Email = "common@mail.com",
                IsActive = true,
                Login = "commonuser",
                Name = "User Common",
                Password = "pwd123",
                RegisterDate = DateTime.Now,
                UserTypeId = 2
            });
            
            context.UserTypeUsers.AddOrUpdate(new UserTypeUser
            {
                Id = 1,
                CreatedDate = DateTime.Now,
                StartDate = DateTime.Now,
                IsActive = true,
                UserId = 1,
                UserTypeId = 1
            });


            context.UserTypeUsers.AddOrUpdate(new UserTypeUser
            {
                Id = 2,
                CreatedDate = DateTime.Now,
                StartDate = DateTime.Now,
                IsActive = true,
                UserId = 2,
                UserTypeId = 2
            });

            context.Database.ExecuteSqlCommand(RemoveProceIfExists);
            context.Database.ExecuteSqlCommand(STP_GET_USER);
            context.Database.ExecuteSqlCommand(STP_REGISTER_USER);

            context.SaveChanges();
        }
    }
}
