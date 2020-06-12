using MassTransit;
using OrderApiCQRS.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApiCQRS.Consumers.EventHandlers
{
    public class OrderCreatedEventHandler : IConsumer<OrderCreatedEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            //TODO: Projection

            await Task.Yield();
        }
    }
}
