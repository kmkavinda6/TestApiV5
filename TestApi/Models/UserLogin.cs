using System.ComponentModel.DataAnnotations;
namespace TestApi.Models

{
    public class UserLogin:User
    {
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

    }
}
