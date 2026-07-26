using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stock.Domain;

namespace Stock.Infrastructure;

public class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Order");

        builder.HasKey(x=> x.Id);

        builder.HasOne(x=> x.Customer)
            .WithMany(x=> x.Orders)
            .HasForeignKey(x=> x.CustomerId);

        builder.Property(x=> x.Code)
            .HasConversion(code=> code.Value, value => OrderCode.Create(value));

        
    }
}
