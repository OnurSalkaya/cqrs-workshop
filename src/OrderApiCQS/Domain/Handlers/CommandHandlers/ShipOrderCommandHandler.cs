using MediatR;
using OrderApiCQS.Domain.Commands;
using OrderApiCQS.Domain.Data.Entities;
using OrderApiCQS.Domain.Data.Repositories;
using OrderApiCQS.Domain.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrderApiCQS.Domain.Handlers.CommandHandlers
{
    public class ShipOrderCommandHandler : IRequestHandler<ShipOrderCommand>
    {
        private readonly IMediator _mediator;
        private readonly IOrderRepository _orderRepository;

        public ShipOrderCommandHandler(IMediator mediator, IOrderRepository orderRepository)
        {
            _mediator = mediator;
            _orderRepository = orderRepository;
        }

        public async Task<Unit> Handle(ShipOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.Get(request.OrderCode);

            //if (order.Status != "Created")
            //{
            //    throw new InvalidOperationException($"Order({request.OrderCode} can not be shipped!");
            //}

            order.Status = "Shipped";

            await _orderRepository.Update(order);

            await _mediator.Publish(new OrderShippedEvent()
            {
                Id = order.Id,
                OrderCode = order.OrderCode,
                OrderDate = order.OrderDate,
                UserId = order.UserId,
                TotalPrice = order.TotalPrice,
                Status = order.Status
            }, cancellationToken);

            return Unit.Value;
        }
    }
}
