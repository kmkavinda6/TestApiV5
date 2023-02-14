using System.ComponentModel.DataAnnotations;

namespace TestApi.Models
{
    public class SalesRep 
    {
        [Key]
        public int SalesRepID { get; set; }

        public string Area { get; set; } = string.Empty;


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
