using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderApiCQS.Domain.Data;
using OrderApiCQS.Domain.Data.Entities;
using OrderApiCQS.Domain.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace OrderApiCQS.Domain.Handlers.QueryHandlers
{
    public class GetOrderByOrderCodeQueryHandler : IRequestHandler<GetOrderByOrderCodeQuery, Order>
    {
        private readonly WorkshopDbContext _dbContext;

        public GetOrderByOrderCodeQueryHandler(WorkshopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> Handle(GetOrderByOrderCodeQuery request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.SingleOrDefaultAsync(x => x.OrderCode == request.OrderCode);

            return order;
        }
    }
}
