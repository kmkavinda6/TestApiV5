using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApi.Models
{
    public class Invoice
    {
        [Key]
        public int invoiceID { get; set; }

        //[ForeignKey]
        public int storeID { get; set; }

        //[ForeignKey]
        public int orderID { get; set; }


        public decimal paidAmount { get; set; }


        public Status status { get; set; }

        public enum Status
        {
            Ongoing,Completed
        }



    }
}
