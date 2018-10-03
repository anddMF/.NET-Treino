using BecaDotNet.Domain.Model;
using BecaDotNet.Domain.Service;
using BecaDotNet.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecaDotNet.ApplicationService
{
    public class UserTypeUserAppSvcGeneric : IGenericService<UserTypeUser>
    {
        private UserTypeUserRepository rep = new UserTypeUserRepository();
        public UserTypeUser Create(UserTypeUser toCreate)
        {
            rep.Create(toCreate);
            rep.Save();
            return toCreate;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserTypeUser> FindBy(UserTypeUser filter)
        {
            throw new NotImplementedException();
        }

        public UserTypeUser Get(int id)
        {
            throw new NotImplementedException();
        }

        public UserTypeUser Update(UserTypeUser toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
