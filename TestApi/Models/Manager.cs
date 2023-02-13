using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace TestApi.Models
{
    [Index(IsUnique = true)]
    public class Manager : User
    {


        
        public int managerID { get; set; }

        public User user { get; set; }


    }
}
