using BecaDotNet.Domain.Model;

namespace BecaDotNet.Domain.Repository
{
    public interface IUserRepository
    {
        User Authenticate(string login, string password);
    }
}
