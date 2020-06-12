using Microsoft.EntityFrameworkCore;
using OrderApiMonolith.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApiMonolith.Data.Repositories
{
    public interface IOrderRepository
    {
        Task Create(Order order);

        Task<Order> Get(string orderCode);
    }

    public class OrderRepository : IOrderRepository
    {
        private readonly WorkshopDbContext _dbContext;

        public OrderRepository(WorkshopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Order order)
        {
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Order> Get(string orderCode)
        {
            return await _dbContext.Orders.SingleOrDefaultAsync(x => x.OrderCode == orderCode);
        }
    }
}
