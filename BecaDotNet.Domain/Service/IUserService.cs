using BecaDotNet.Domain.Model;

namespace BecaDotNet.Domain.Service
{
    public interface IUserService
    {
        ResultModel RegisterUser(User newUser);
        ResultModel AuthenticateUser(string login, string password);
        ResultModel UpdateUser(User updatedUser);
        ResultModel RemoveUser(int userId);
        ResultModel GetUser(int userId);
        ResultModel GetUser(User filter);
    }
}
