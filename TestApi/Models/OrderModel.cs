using System.Collections.Generic;

namespace TestApi.Models
{
    public class OrderModel
    {
        public string StoreName { get; set; }
        public int SalesRepId { get; set; }
        public List<ItemQtyModel> ItemQuantities { get; set; }
    }
}
