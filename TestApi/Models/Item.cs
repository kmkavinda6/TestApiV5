using System.ComponentModel.DataAnnotations;
using System;

namespace TestApi.Models
{
    public class Item
    {
        [Key]
        public int itemID { get; set; }

        [Required]
        public string name { get; set; } = string.Empty;

        [Required]
        public int qty { get; set; }

        [Required]
        public DateTime expDate { get; set; }

        [Required]
        public int price { get; set; }

        [Required]
        public int batchID { get; set; }
    }
}
