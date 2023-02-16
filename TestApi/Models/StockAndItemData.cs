using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestApi.Models
{
    public class StockAndItemData
    {
        [Required]
        public int ManagerId { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public List<Item> Items { get; set; }
    }
}
