using System;
using System.ComponentModel.DataAnnotations;

namespace TestApi.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [Required]
        public int StoreID { get; set; }

        [Required]
        public int SalesRepID { get; set; }

        [Required]
        public int ItemID { get; set; }

        public decimal TotalAmount { get; set; }

        [Required]
        public int Qty { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public bool IsDeleverd { get; set; }

    }

}
