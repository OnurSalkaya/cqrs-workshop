using MassTransit;
using OrderApi.Domain.Events;
using OrderQueryApi.Data.Repositories;
using OrderQueryApi.Models.Projections;
using System.Threading.Tasks;

namespace OrderQueryApi.Projections
{
    public class OrderShippedEventHandler : IConsumer<OrderShippedEvent>
    {
        private readonly IListingOrderRepository _listingOrderRepository;

        public OrderShippedEventHandler(IListingOrderRepository listingOrderRepository)
        {
            _listingOrderRepository = listingOrderRepository;
        }

        public async Task Consume(ConsumeContext<OrderShippedEvent> context)
        {
            await _listingOrderRepository.UpdateStatus(context.Message.OrderCode, context.Message.Status);
        }
    }
}
