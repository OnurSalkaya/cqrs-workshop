using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.Domain.Events
{
    public class OrderCreatedEvent
    {
        public Guid Id { get; private set; }

        public string OrderCode { get; private set; }

        public DateTime OrderDate { get; private set; }

        public int UserId { get; private set; }

        public decimal TotalPrice { get; private set; }
    }
}
