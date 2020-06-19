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

        //Added for dbContext.Database.EnsureCreated(); because there must be parameterless constructor.
        public Order()
        {

        }

        public Order(Guid id, string orderCode, DateTime orderDate, int userId, decimal totalPrice)
        {
            ApplyChange(new OrderCreatedEvent(id, orderCode, orderDate, userId, totalPrice));
        }

        public static Order Create(Guid id, string orderCode, DateTime orderDate, int userId, decimal totalPrice)
        {
            var order = new Order()
            {
                _id = id,
                OrderCode = orderCode,
                OrderDate = orderDate,
                TotalPrice = totalPrice,
                UserId = userId
            };

            order.ApplyChange(new OrderCreatedEvent(id, orderCode, orderDate, userId, totalPrice));

            return order;
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
                    break;
            }
        }

        public string OrderCode { get; private set; }

        public DateTime OrderDate { get; private set; }

        public int UserId { get; private set; }

        public decimal TotalPrice { get; private set; }


    }
}
