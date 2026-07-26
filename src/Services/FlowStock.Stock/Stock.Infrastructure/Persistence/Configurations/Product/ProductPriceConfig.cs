using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stock.Domain;

namespace Stock.Infrastructure;

public class ProductPriceConfig : IEntityTypeConfiguration<ProductPrice>
{
    public void Configure(EntityTypeBuilder<ProductPrice> builder)
    {
        builder.ToTable("ProductPrice");

        builder.HasKey(x=> x.Id);

        builder.HasOne(x=> x.Product)
            .WithMany(x=> x.ProductPrices)
            .HasForeignKey(x=> x.ProductId);
            
    }
}
