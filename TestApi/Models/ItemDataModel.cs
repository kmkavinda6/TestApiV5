using System.ComponentModel.DataAnnotations;
using System;

namespace TestApi.Models
{
    public class ItemDataModel
    {
        [Required]
        public string ItemName { get; set; }

        [Required]
        public int Qty { get; set; }

        [Required]
        public DateTime ExpireDate { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
