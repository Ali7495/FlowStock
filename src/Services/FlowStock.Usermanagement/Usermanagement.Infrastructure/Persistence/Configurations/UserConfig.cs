using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Usermanagement.Domain;

namespace Usermanagement.Infrastructure;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Username)
        .IsRequired();

        builder.Property(x => x.HashedPassword)
        .IsRequired();

        builder.Property(x => x.Email)
        .HasConversion(email => email.Value, value => Email.Create(value));

        builder.HasOne(x => x.Person)
        .WithMany(x => x.Users)
        .HasForeignKey(x => x.PersonId);
    }
}
