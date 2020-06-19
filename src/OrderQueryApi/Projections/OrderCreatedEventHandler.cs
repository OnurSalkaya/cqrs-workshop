using MassTransit;
using OrderApi.Domain.Events;
using OrderQueryApi.Data.Repositories;
using OrderQueryApi.Models.Projections;
using System.Threading.Tasks;

namespace OrderQueryApi.Projections
{
    public class OrderCreatedEventHandler : IConsumer<OrderCreatedEvent>
    {
        private readonly IListingOrderRepository _listingOrderRepository;

        public OrderCreatedEventHandler(IListingOrderRepository listingOrderRepository)
        {
            _listingOrderRepository = listingOrderRepository;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var listingOrderDocument = new ListingOrder()
            {
                OrderId = context.Message.Id,
                OrderCode = context.Message.OrderCode,
                OrderDate = context.Message.OrderDate,
                UserId = context.Message.UserId,
                TotalPrice = context.Message.TotalPrice,
            };

            await _listingOrderRepository.Insert(listingOrderDocument);
        }
    }
}
