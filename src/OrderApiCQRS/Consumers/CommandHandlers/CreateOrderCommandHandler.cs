using MassTransit;
using OrderApiCQRS.Domain.Commands;
using OrderApiCQRS.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApiCQRS.Consumers.CommandHandlers
{
    public class CreateOrderCommandHandler : IConsumer<CreateOrderCommand>
    {
        public async Task Consume(ConsumeContext<CreateOrderCommand> context)
        {
            //TODO: Insert to DB

            var orderCreatedEvent = new OrderCreatedEvent()
            {
                Id = 9999,
                OrderCode = context.Message.OrderCode,
                OrderDate = context.Message.OrderDate,
                UserId = context.Message.UserId,
                TotalPrice = context.Message.TotalPrice
            };

            await context.Publish(orderCreatedEvent);
        }
    }
}
