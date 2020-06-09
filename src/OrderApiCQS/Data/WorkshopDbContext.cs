using Microsoft.EntityFrameworkCore;
using OrderApiCQS.Data.Entities;
using OrderApiCQS.Data.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApiCQS.Data
{
    public class WorkshopDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public WorkshopDbContext(DbContextOptions<WorkshopDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
