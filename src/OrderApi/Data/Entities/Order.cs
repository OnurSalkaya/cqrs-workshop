using OrderApi.Data.Repositories.MsSql;
using OrderApi.Domain.Core;
using OrderApi.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.Data.Entities
{
    public class Order : AggregateRoot
    {
        private Guid _id;

        public static Order Create(Guid id, string orderCode, DateTime orderDate, int userId, decimal totalPrice)
        {
            var order = new Order()
            {
                _id = id,
                OrderCode = orderCode,
                OrderDate = orderDate,
                TotalPrice = totalPrice,
                UserId = userId,
                Status = "Created"
            };

            order.ApplyChange(new OrderCreatedEvent(id, orderCode, orderDate, userId, totalPrice, "Created"));

            return order;
        }

        public void SetStatusAsShipped()
        {
            //if (Status != "Created")
            //{
            //    throw new InvalidOperationException($"Order({request.OrderCode} can not be shipped!");
            //}

            ApplyChange(new OrderShippedEvent(_id, OrderCode, OrderDate, UserId, TotalPrice, "Shipped"));
        }

        public override Guid Id
        {
            get
            {
                return _id;
            }
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case OrderCreatedEvent e:
                    _id = e.Id;
                    OrderCode = e.OrderCode;
                    OrderDate = e.OrderDate;
                    UserId = e.UserId;
                    TotalPrice = e.TotalPrice;
                    Status = e.Status;
                    break;
                case OrderShippedEvent e:
                    Status = e.Status;
                    break;
            }
        }

        public string OrderCode { get; private set; }

        public DateTime OrderDate { get; private set; }

        public int UserId { get; private set; }

        public decimal TotalPrice { get; private set; }

        public string Status { get; private set; }
    }
}
