using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stock.Domain;

namespace Stock.Infrastructure;

public class InvoiceItemConfig : IEntityTypeConfiguration<InvoiceItem>
{
    public void Configure(EntityTypeBuilder<InvoiceItem> builder)
    {
        builder.ToTable("InvoiceItem");

        builder.HasKey(x=> x.Id);

        builder.HasOne(x=> x.Invoice)
            .WithMany(x=> x.InvoiceItems)
            .HasForeignKey(x=> x.InvoiceId);

        builder.HasOne(x=> x.Product)
            .WithMany(x=> x.InvoiceItems)
            .HasForeignKey(x=> x.ProductId);    
    }
}
