using MediatR;
using OrderApiCQS.Domain.Commands;
using OrderApiCQS.Domain.Data.Entities;
using OrderApiCQS.Domain.Data.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace OrderApiCQS.Domain.Handlers.CommandHandlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Order order = new Order()
                {
                    OrderCode = request.OrderCode,
                    OrderDate = request.OrderDate,
                    TotalPrice = request.TotalPrice
                };

                await _orderRepository.Create(order);
                return order;
            }
            catch
            {
                return null;
            }
        }
    }
}
