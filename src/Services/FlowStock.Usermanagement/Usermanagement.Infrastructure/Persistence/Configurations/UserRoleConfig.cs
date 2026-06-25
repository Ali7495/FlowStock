using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Usermanagement.Domain;

namespace Usermanagement.Infrastructure;

public class UserRoleConfig : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRole");
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.User)
        .WithMany(x => x.UserRoles)
        .HasForeignKey(x => x.UserId);

        builder.HasOne(x=> x.Role)
        .WithMany(x=> x.UserRoles)
        .HasForeignKey(x=> x.RoleId);
    }
}
