using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using OrderApiCQRS.Domain.Commands;
using OrderApiCQRS.Extensions;
using OrderApiCQRS.Models;
using OrderApiMonolith.Data.Entities;
using OrderApiMonolith.Data.Repositories;

namespace OrderApiMonolith.Services
{
    public interface IOrderService
    {
        Task CreateOrder(OrderRequest orderRequest);

        //Task<Order> GetOrder(string orderCode);
    }

    public class OrderService : IOrderService
    {
        private IBusControl _busControl;

        public OrderService(IBusControl busControl)
        {
            _busControl = busControl;
        }

        public async Task CreateOrder(OrderRequest orderRequest)
        {
            var createOrderCommand = new CreateOrderCommand()
            {
                OrderCode = orderRequest.OrderCode,
                OrderDate = orderRequest.OrderDate,
                UserId = orderRequest.UserId,
                TotalPrice = orderRequest.TotalPrice
            };

            await _busControl.Send(createOrderCommand, "create-order-command-queue");
        }

        //public async Task<Order> GetOrder(string orderCode)
        //{
        //    return await _orderRepository.Get(orderCode);
        //}
    }
}
