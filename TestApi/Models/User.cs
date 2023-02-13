using System.ComponentModel.DataAnnotations;

namespace TestApi.Models
{
    public class User
    {

        [Key]
        public int UserID { get; set; }

        
        public string userName { get; set; } = string.Empty;

        public string fName { get; set; } = string.Empty;

        public string Lname { get; set; } = string.Empty;

        public string address { get; set; } = string.Empty;

        public string phoneNo { get; set; } = string.Empty;

        [Required]
        public string userType { get; set; }

        public string nic { get; set; } = string.Empty;

        [DataType(DataType.EmailAddress)]
        public string email { get; set; } = string.Empty;

        /*  public enum UserType { 

              Admin,Manager,SalesRep
          }*/

       /* public ICollection<Admin> Admins { get; set; }
        public ICollection<Manager> Managers { get; set; }
        public ICollection<SalesRep> SalesReps { get; set; }

        public int? AdminId { get; set; }
        public Admin Admin { get; set; }

        public int? ManagerId { get; set; }
        public Manager Manager { get; set; }


        public int? SalesRepId { get; set; }
        public SalesRep SalesRep { get; set; }*/




    }
}
