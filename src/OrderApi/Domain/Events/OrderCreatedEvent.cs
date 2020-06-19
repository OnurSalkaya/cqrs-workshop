using OrderApi.Data.Entities;
using OrderApi.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.Domain.Events
{
    public class OrderCreatedEvent
    {
        public OrderCreatedEvent(Guid id,
            string orderCode,
            DateTime orderDate,
            int userId,
            decimal totalPrice)
        {
            Id = id;
            OrderCode = orderCode;
            OrderDate = orderDate;
            UserId = userId;
            TotalPrice = totalPrice;
        }

        public Guid Id { get; }

        public string OrderCode { get; }

        public DateTime OrderDate { get; }

        public int UserId { get; }

        public decimal TotalPrice { get; }
    }
}
