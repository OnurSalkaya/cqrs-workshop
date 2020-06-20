using Microsoft.EntityFrameworkCore;
using OrderApiCQS.Domain.Data.Entities;
using System;
using System.Threading.Tasks;

namespace OrderApiCQS.Domain.Data.Repositories
{
    public interface IOrderRepository
    {
        Task Create(Order order);

        Task<Order> Get(string orderCode);

        Task Update(Order order);
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

        public async Task Update(Order order)
        {
            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();
        }
    }
}
