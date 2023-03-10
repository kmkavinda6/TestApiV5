using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApi.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [Required]
        public int StoreID { get; set; }

        [ForeignKey("StoreID")]
        public virtual Store Store { get; set; }

        [Required]
        public int SalesRepID { get; set; }

        public decimal TotalAmount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public bool IsDelivered { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; }  // Add this line
    }

}
