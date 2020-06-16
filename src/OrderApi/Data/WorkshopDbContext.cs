using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderApi.Data.Entities;
using OrderApi.Data.Mappings;

namespace OrderApi.Data
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
