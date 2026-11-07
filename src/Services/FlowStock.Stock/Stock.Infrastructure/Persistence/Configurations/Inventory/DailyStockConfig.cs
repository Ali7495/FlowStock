using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stock.Domain;

namespace Stock.Infrastructure;

public sealed class DailyStockConfig : IEntityTypeConfiguration<DailyStock>
{
    public void Configure(EntityTypeBuilder<DailyStock> builder)
    {
        builder.ToTable("DailyStock");

        builder.HasKey(x=> x.Id);

        builder.HasOne(x=> x.Product)
            .WithMany(x=> x.DailyStocks)
            .HasForeignKey(x=> x.ProductId);

        builder.HasIndex(x=> x.ProductId);    
    }
}
