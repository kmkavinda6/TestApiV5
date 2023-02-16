using System;

namespace TestApi.Models
{
    public class OrderViewModel
    {
        public int OrderID { get; set; }
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public int SalesRepID { get; set; }
        public DateTime Date { get; set; }
        public bool IsDelivered { get; set; }
    }
}
