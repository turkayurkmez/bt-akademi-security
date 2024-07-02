using AuthNAndAuthZ.Models;

namespace AuthNAndAuthZ.Services
{
    public interface IUserService
    {
        void CreateUser(CreateNewUserModel model);
        User? ValidateUser(string username, string password);
    }
}