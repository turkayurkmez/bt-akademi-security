using System.ComponentModel.DataAnnotations;

namespace AuthNAndAuthZ.Models
{
    public class CreateNewUserModel
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string ClearPassword { get; set; }
        //public string? Role { get; set; }
    }
}
