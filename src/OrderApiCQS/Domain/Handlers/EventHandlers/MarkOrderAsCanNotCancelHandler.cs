using MediatR;
using OrderApiCQS.Domain.Data.Repositories;
using OrderApiCQS.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderApiCQS.Domain.Handlers.EventHandlers
{
    public class MarkOrderAsCanNotCancelHandler : INotificationHandler<OrderShippedEvent>
    {
        private readonly IOrderRepository _orderRepository;

        public MarkOrderAsCanNotCancelHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task Handle(OrderShippedEvent notification, CancellationToken cancellationToken)
        {
            var order = _orderRepository.Get(notification.OrderCode);

            //order.IsCancellable = false;

            //ETC...

            await Task.Yield();
        }
    }
}
