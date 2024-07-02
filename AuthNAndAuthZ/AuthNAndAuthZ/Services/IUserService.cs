using AuthNAndAuthZ.Models;

namespace AuthNAndAuthZ.Services
{
    public interface IUserService
    {
        User? ValidateUser(string username, string password);
    }
}