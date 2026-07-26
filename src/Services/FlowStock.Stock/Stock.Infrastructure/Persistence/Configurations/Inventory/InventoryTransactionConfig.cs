using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stock.Domain;

namespace Stock.Infrastructure;

public class InventoryTransactionConfig : IEntityTypeConfiguration<InventoryTransaction>
{
    public void Configure(EntityTypeBuilder<InventoryTransaction> builder)
    {
        builder.ToTable("InventoryTransaction");

        builder.HasKey(x=> x.Id);

        builder.HasOne(x=> x.Product)
            .WithMany(x=> x.InventoryTransactions)
            .HasForeignKey(x=> x.ProductId);
    }
}
