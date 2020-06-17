using MediatR;
using OrderApiCQS.Domain.Commands;
using OrderApiCQS.Domain.Data.Entities;
using OrderApiCQS.Domain.Data.Repositories;
using OrderApiCQS.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace OrderApiCQS.Domain.Handlers.CommandHandlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
    {
        private readonly IMediator _mediator;
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(IMediator mediator, IOrderRepository orderRepository)
        {
            _mediator = mediator;
            _orderRepository = orderRepository;
        }

        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            Order order = new Order()
            {
                OrderCode = request.OrderCode,
                OrderDate = request.OrderDate,
                UserId = request.UserId,
                TotalPrice = request.TotalPrice
            };

            await _orderRepository.Create(order);

            await _mediator.Publish(new OrderCreatedEvent()
            {
                Id = order.Id,
                OrderCode = order.OrderCode,
                OrderDate = order.OrderDate,
                UserId = order.UserId,
                TotalPrice = order.TotalPrice
            }, cancellationToken);

            return Unit.Value;
        }
    }
}
