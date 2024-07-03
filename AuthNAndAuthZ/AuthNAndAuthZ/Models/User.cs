using System.ComponentModel.DataAnnotations.Schema;

namespace AuthNAndAuthZ.Models
{
    [Table("RealUsers")]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        [NotMapped]
        public string Password { get; set; }
        public string? Role { get; set; }

        public string PasswordHash { get; set; }
    }
}
