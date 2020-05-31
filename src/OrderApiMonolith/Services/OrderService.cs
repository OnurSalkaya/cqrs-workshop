using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderApiMonolith.Data.Entities;
using OrderApiMonolith.Data.Repositories;

namespace OrderApiMonolith.Services
{
    public interface IOrderService
    {
        Task CreateOrder(Order order);

        Task<Order> GetOrder(string orderCode);
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task CreateOrder(Order order)
        {
            await _orderRepository.Create(order);
        }

        public async Task<Order> GetOrder(string orderCode)
        {
            return await _orderRepository.Get(orderCode);
        }
    }
}
