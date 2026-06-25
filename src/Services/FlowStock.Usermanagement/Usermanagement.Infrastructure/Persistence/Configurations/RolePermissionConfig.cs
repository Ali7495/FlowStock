using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Usermanagement.Domain;

namespace Usermanagement.Infrastructure;

public class RolePermissionConfig : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("RolePermission");
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Role)
        .WithMany(x => x.RolePermissions)
        .HasForeignKey(x => x.RoleId);

        builder.HasOne(x => x.Permission)
        .WithMany(x => x.RolePermissions)
        .HasForeignKey(x => x.PermissionId);
    }
}
