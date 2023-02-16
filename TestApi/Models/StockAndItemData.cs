using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestApi.Models
{
    public class StockAndItemData
    {
        public int ManagerId { get; set; }
        public string CompanyName { get; set; }
        public List<ItemDataModel> Items { get; set; }
    }
}
