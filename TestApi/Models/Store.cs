using System.ComponentModel.DataAnnotations;

namespace TestApi.Models { 
    public class Store
    {
        [Key]
        public int StoreID { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string PhoneNo { get; set; } = string.Empty;

        public string OwnerName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string BRegNo {get; set; } = string.Empty;

    }
}
