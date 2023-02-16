using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace TestApi.Models
{
    public class StockModel
    {
       
            [Required]
            public int ManagerId { get; set; }

            [Required]
            public string CompanyName { get; set; }

            [Required]
            public DateTime Date { get; set; }

            [Required]
            public List<ItemDataModel> ItemData { get; set; }
        
    }
}
