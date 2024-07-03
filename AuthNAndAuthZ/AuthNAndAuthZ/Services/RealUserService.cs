using AuthNAndAuthZ.DataContext;
using AuthNAndAuthZ.Models;
using BCrypt.Net;

namespace AuthNAndAuthZ.Services
{
    public class RealUserService : IUserService
    {

        SecureDbContext dbContext;

        public RealUserService(SecureDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void CreateUser(CreateNewUserModel model)
        {
            //Kullanıcının input'a girdiği parolayı hash'leyerek veritabanına kaydet:
            var user = new User
            {
                Email = model.Email,
                Name = model.Name,
                UserName = model.UserName,
                Role = "Client"

            };

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.ClearPassword);

            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }

        public User? ValidateUser(string username, string password)
        {
            var user = dbContext.Users.SingleOrDefault(u => u.UserName == username);
            if (user != null)
            {
                var isVerified = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
                return isVerified ? user : null;
            }

            return null;

        }
    }
}
