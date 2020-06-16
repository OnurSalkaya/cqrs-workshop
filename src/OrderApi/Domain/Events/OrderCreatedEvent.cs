using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.Domain.Events
{
    public class OrderCreatedEvent
    {
        public int Id { get; set; }

        public string OrderCode { get; set; }

        public DateTime OrderDate { get; set; }

        public int UserId { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
