using AuthNAndAuthZ.Models;

namespace AuthNAndAuthZ.Services
{
    public class UserService : IUserService
    {
        private List<User> users = new()
        {
            new() { Id = 1, Name="Türkay", UserName="turkay", Email="a@b.com", Password="123456", Role="Admin" },
            new() { Id = 2, Name="Burçin", UserName="burcina", Email="a@b.com", Password="123456", Role="Editor" },
            new() { Id = 3, Name="Yiğitcan", UserName="ycan", Email="a@b.com", Password="123456", Role="Client" }
        };


        public User? ValidateUser(string username, string password)
        {
            return users.SingleOrDefault(x => x.UserName == username && x.Password == password);
        }

        public void CreateUser(CreateNewUserModel model)
        {

        }


    }
}
