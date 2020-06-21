using MassTransit;
using OrderApi.Data.Entities;
using OrderApi.Data.Repositories.MsSql;
using OrderApi.Domain.Commands;
using System.Threading.Tasks;

namespace OrderApi.Consumers.CommandHandlers
{
    public class ShipOrderCommandHandler : IConsumer<ShipOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public ShipOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task Consume(ConsumeContext<ShipOrderCommand> context)
        {
            var order = await _orderRepository.Get(context.Message.OrderCode);

            //if (order.Status != "Created")
            //{
            //    throw new InvalidOperationException($"Order({request.OrderCode} can not be shipped!");
            //}

            order.SetStatusAsShipped();

            await _orderRepository.Update(order);
        }
    }
}
