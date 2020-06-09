using System;

namespace OrderApiCQS.Domain.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public string OrderCode { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
