using BecaDotNet.Domain.Model;
using BecaDotNet.Domain.Repository;
using BecaDotNet.Repository.Context;
using System.Linq;

namespace BecaDotNet.Repository
{
    public class UserRepositoryGeneric : GenericRepository<BecaContext, User>,
        IUserRepository
    {
        public User Authenticate(string login, string password)
        {
            var result = Context.Set<User>().FirstOrDefault(
                f => f.Login.Equals(login) &&
                f.Password.Equals(password));
            return result;
        }
    }
}
