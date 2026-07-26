using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stock.Domain;

namespace Stock.Infrastructure;

public class InventoryReservationConfig : IEntityTypeConfiguration<InventoryReservation>
{
    public void Configure(EntityTypeBuilder<InventoryReservation> builder)
    {
        builder.ToTable("InventoryReservation");

        builder.HasKey(x=> x.Id);

        builder.HasOne(x=> x.Order)
            .WithMany(x=> x.InventoryReservations)
            .HasForeignKey(x=> x.OrderId);
    }
}
