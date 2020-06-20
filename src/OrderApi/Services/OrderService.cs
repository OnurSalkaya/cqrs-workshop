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
using Nest;

namespace OrderApi.Services
{
    public interface IOrderService
    {
        Task CreateOrder(CreateOrderRequest createOrderRequest);

        Task ShipOrder(ShipOrderRequest shipOrderRequest);
    }

    public class OrderService : IOrderService
    {
        private readonly IBusControl _busControl;

        public OrderService(IBusControl busControl)
        {
            _busControl = busControl;
        }

        public async Task CreateOrder(CreateOrderRequest createOrderRequest)
        {
            var createOrderCommand = new CreateOrderCommand()
            {
                Id = Guid.NewGuid(),
                OrderCode = createOrderRequest.OrderCode,
                OrderDate = createOrderRequest.OrderDate,
                UserId = createOrderRequest.UserId,
                TotalPrice = createOrderRequest.TotalPrice
            };

            await _busControl.Send(createOrderCommand, "create-order-command-queue");
        }

        public async Task ShipOrder(ShipOrderRequest shipOrderRequest)
        {
            var shipOrderCommand = new ShipOrderCommand()
            {
                OrderCode = shipOrderRequest.OrderCode,
            };

            await _busControl.Send(shipOrderCommand, "ship-order-command-queue");
        }
    }
}
