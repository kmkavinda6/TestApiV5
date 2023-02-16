using System.ComponentModel.DataAnnotations;

namespace TestApi.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemID { get; set; }

        [Required]
        public int OrderID { get; set; }
        public virtual Order Order { get; set; }

        [Required]
        public int ItemID { get; set; }
        public virtual Item Item { get; set; }

        [Required]
        public int Qty { get; set; }
    }
}
