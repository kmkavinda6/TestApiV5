using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApi.Models
{
    //[Index(IsUnique = true)]
    public class Admin : User
    {

        /*[ForeignKey("User")]
        public int UserID { get; set; }*/
        public int adminID { get; set; }

        public virtual User User { get; set; }
        /*public User? user { get; set; }

        [ForeignKey("userID")]
        public int userID { get; set; }*/



    }
}
