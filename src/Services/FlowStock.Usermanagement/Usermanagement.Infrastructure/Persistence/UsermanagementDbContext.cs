using Microsoft.EntityFrameworkCore;
using Usermanagement.Domain;

namespace Usermanagement.Infrastructure;

public class UsermanagementDbContext : DbContext
{
    #region DbSets

    #region UserPerson

    DbSet<Person> Persons => Set<Person>();
    DbSet<User> Users => Set<User>();
    DbSet<UserRole> UserRoles => Set<UserRole>();

    #endregion

    #region RolePermission

    DbSet<Permission> Permissions => Set<Permission>();
    DbSet<Role> Roles => Set<Role>();
    DbSet<RolePermission> RolePermissions => Set<RolePermission>();

    #endregion

    #region Token

    DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    #endregion

    #endregion

    public UsermanagementDbContext(DbContextOptions<UsermanagementDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsermanagementDbContext).Assembly);
    }

}
