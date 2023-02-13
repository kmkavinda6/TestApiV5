using System.ComponentModel.DataAnnotations;

namespace TestApi.Models { 
    public class Store
    {
        [Key]
        public int storeID { get; set; }

        [Required]
        public string name { get; set; } = string.Empty;

        public string phoneNo { get; set; } = string.Empty;

        public string ownerName { get; set; } = string.Empty;

        public string address { get; set; } = string.Empty;

        public string bRegNo {get; set; } = string.Empty;

    }
}
