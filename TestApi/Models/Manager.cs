using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace TestApi.Models
{
    
    public class Manager 
    {


        [Key]
        public int ManagerID { get; set; }


        public string UserName { get; set; } = string.Empty;

        public string FName { get; set; } = string.Empty;

        public string Lname { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string PhoneNo { get; set; } = string.Empty;

        public string Nic { get; set; } = string.Empty;

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;


    }
}
