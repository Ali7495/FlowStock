using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stock.Domain;

namespace Stock.Infrastructure;

public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.ProductCategory)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.ProductCategoryId);

        builder.Property(x => x.Name).IsRequired();

        builder.Property(x => x.Code)
            .HasConversion(code => code.Value, value => ProductCode.Create(value));
    }
}
