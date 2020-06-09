using MediatR;
using OrderApiCQS.Commands;
using OrderApiCQS.Data;
using OrderApiCQS.Data.Entities;
using OrderApiCQS.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderApiCQS.Handlers
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
