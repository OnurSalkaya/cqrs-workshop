using OrderApi.Data.Entities;
using OrderApi.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.Domain.Events
{
    public class OrderShippedEvent
    {
        public OrderShippedEvent(Guid id,
            string orderCode,
            DateTime orderDate,
            int userId,
            decimal totalPrice,
            string status)
        {
            Id = id;
            OrderCode = orderCode;
            OrderDate = orderDate;
            UserId = userId;
            TotalPrice = totalPrice;
            Status = status;
        }

        public Guid Id { get; }

        public string OrderCode { get; }

        public DateTime OrderDate { get; }

        public int UserId { get; }

        public decimal TotalPrice { get; }

        public string Status { get; set; }
    }
}
