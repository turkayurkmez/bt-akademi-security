using System.ComponentModel.DataAnnotations;

namespace InjectionAttacks.Models
{
    public class UserRegisterViewModel  
    {
        [Required(ErrorMessage ="İsminizi unutmayın")]
        public string Name { get; set; }
        public string Email { get; set; }
        [DataType(DataType.MultilineText)]
        public string UserInfo { get; set; }
    }
}
