using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stock.Domain;

namespace Stock.Infrastructure;

public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItem");

        builder.HasKey(x=> x.Id);

        builder.HasOne(x=> x.ProductPrice)
            .WithMany(x=> x.OrderItems)
            .HasForeignKey(x=> x.ProductPriceId);

        builder.HasOne(x=> x.Order)
            .WithMany(x=> x.OrderItems)
            .HasForeignKey(x=> x.OrderId);    
    }
}
