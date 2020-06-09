using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderApiCQS.Domain.Data.Entities;

namespace OrderApiCQS.Domain.Data.Mappings
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order", "dbo");

            builder.HasKey(p => p.Id); //Primary key and idendity

            builder.Property(p => p.OrderCode)
                .HasMaxLength(255)
                .IsRequired(true);

            builder.Property(p => p.OrderDate)
                .HasColumnType("DateTime")
                .IsRequired(true);

            builder.Property(p => p.TotalPrice)
                .HasColumnType("Money")
                .IsRequired(true);
        }
    }
}
