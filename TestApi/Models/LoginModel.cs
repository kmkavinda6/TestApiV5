using TestApi.Models;
using System.ComponentModel.DataAnnotations;

namespace TestApi.Models
{
    public class LoginModel:User
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}

