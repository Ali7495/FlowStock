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


        #region QueryFilters

        modelBuilder.Entity<Person>().HasQueryFilter(u=> !u.IsDeleted);
        modelBuilder.Entity<User>().HasQueryFilter(u=> !u.IsDeleted);
        modelBuilder.Entity<Role>().HasQueryFilter(u=> !u.IsDeleted);
        modelBuilder.Entity<UserRole>().HasQueryFilter(u=> !u.IsDeleted);
        modelBuilder.Entity<Permission>().HasQueryFilter(u=> !u.IsDeleted);
        modelBuilder.Entity<RolePermission>().HasQueryFilter(u=> !u.IsDeleted);
        modelBuilder.Entity<RefreshToken>().HasQueryFilter(u=> !u.IsDeleted);

        #endregion

        #region Indecies

        modelBuilder.Entity<User>().HasIndex(i=> new
        {
            i.Username, i.Email
        });

        #endregion
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BasicEntity>())
        {
            if(entry.State == EntityState.Added)
            {
                entry.Entity.Id = Guid.NewGuid();
                entry.Entity.CreatedAt = DateTime.UtcNow;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }

            if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;

                entry.Entity.IsDeleted = true;
                entry.Entity.DeletedAt = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

}
