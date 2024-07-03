namespace SecurityInREST.Services
{
    public class UserService
    {
        private List<User> users = new List<User>()
        {
            new(){ Id=1, Name="Emre", Password="12345", Role="Admin", UserName="emre"},
            new(){ Id=2, Name="Turkay", Password="12345", Role="Editor", UserName="turkay"},
            new(){ Id=3, Name="Ünal", Password="12345", Role="Client", UserName="unal"},


        };

        public User? Validate(string userName,  string password)=> users.SingleOrDefault(u=>u.UserName == userName && u.Password == password);

    }
}
