using System.ComponentModel.DataAnnotations;

namespace TestApi.Models
{
    public class Register
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(password),ErrorMessage = "Passwords missmatch")]
        public string confirmPassword { get; set; } = string.Empty;


        //get
    }
}
