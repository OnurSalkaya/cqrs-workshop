using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderApiCQS.Data;
using OrderApiCQS.Data.Entities;
using OrderApiCQS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderApiCQS.Handlers
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
