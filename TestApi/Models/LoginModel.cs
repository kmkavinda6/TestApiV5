using TestApi.Models;
using System.ComponentModel.DataAnnotations;

namespace TestApi.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? userName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? password { get; set; }
        
    }
}

