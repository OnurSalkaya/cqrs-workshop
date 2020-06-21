using MassTransit;
using OrderApi.Domain.Commands;
using OrderApi.Domain.Events;
using OrderApi.Data.Entities;
using OrderApi.Data.Repositories.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.Consumers.CommandHandlers
{
    public class ShipOrderCommandHandler : IConsumer<ShipOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public ShipOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task Consume(ConsumeContext<ShipOrderCommand> context)
        {
            var order = await _orderRepository.Get(context.Message.OrderCode);
            order.Status = "Shipped";

            await _orderRepository.Update(order);

            //publish event
            var orderCreatedEvent = new OrderShippedEvent()
            {
                Id = order.Id,
                OrderCode = order.OrderCode,
                OrderDate = order.OrderDate,
                UserId = order.UserId,
                TotalPrice = order.TotalPrice,
                Status = order.Status
            };

            await context.Publish(orderCreatedEvent);
        }
    }
}
