using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using OrderApi.Domain.Commands;
using OrderApi.Extensions;
using OrderApi.Models;
using OrderApi.Data.Entities;
using OrderApi.Data.Repositories;

namespace OrderApi.Services
{
    public interface IOrderService
    {
        Task CreateOrder(OrderRequest orderRequest);
    }

    public class OrderService : IOrderService
    {
        private readonly IBusControl _busControl;

        public OrderService(IBusControl busControl)
        {
            _busControl = busControl;
        }

        public async Task CreateOrder(OrderRequest orderRequest)
        {
            var createOrderCommand = new CreateOrderCommand()
            {
                Id = Guid.NewGuid(),
                OrderCode = orderRequest.OrderCode,
                OrderDate = orderRequest.OrderDate,
                UserId = orderRequest.UserId,
                TotalPrice = orderRequest.TotalPrice
            };

            await _busControl.Send(createOrderCommand, "create-order-command-queue");
        }
    }
}
