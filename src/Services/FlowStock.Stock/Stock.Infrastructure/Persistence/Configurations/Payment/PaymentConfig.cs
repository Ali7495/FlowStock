using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stock.Domain;

namespace Stock.Infrastructure;

public class PaymentConfig : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payment");

        builder.HasKey(x=> x.Id);

        builder.HasOne(x=> x.Order)
            .WithMany(x=> x.Payments)
            .HasForeignKey(x=> x.OrderId);

        builder.Property(x=> x.PaymentCode)
            .HasConversion(code => code.Value, value => PaymentCode.Create(value));
    }
}
