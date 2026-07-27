using Microsoft.EntityFrameworkCore;
using Stock.Domain;

namespace Stock.Infrastructure;

public class StockDbContext : DbContext
{

    #region DbSets

    DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
    DbSet<Product> Products => Set<Product>();
    DbSet<ProductPrice> ProductPrices => Set<ProductPrice>();
    DbSet<Customer> Customers => Set<Customer>();
    DbSet<Order> Orders => Set<Order>();
    DbSet<OrderItem> OrderItems => Set<OrderItem>();
    DbSet<Payment> Payments => Set<Payment>();
    DbSet<Invoice> Invoices => Set<Invoice>();
    DbSet<InvoiceItem> InvoiceItems => Set<InvoiceItem>();
    DbSet<InventoryReservation> InventoryReservations => Set<InventoryReservation>();
    DbSet<InventoryTransaction> InventoryTransactions => Set<InventoryTransaction>();

    #endregion

    public StockDbContext(DbContextOptions<StockDbContext> options) : base(options)
    {

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StockDbContext).Assembly);

        #region QueryFilters

        modelBuilder.Entity<ProductCategory>().HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<ProductPrice>().HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<Customer>().HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<Order>().HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<OrderItem>().HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<Payment>().HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<Invoice>().HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<InvoiceItem>().HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<InventoryReservation>().HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<InventoryTransaction>().HasQueryFilter(p => !p.IsDeleted);

        #endregion

        #region Indecies

        modelBuilder.Entity<Order>().HasIndex(o => o.Code);
        modelBuilder.Entity<Customer>().HasIndex(c => c.PersonId);
        modelBuilder.Entity<Payment>().HasIndex(p => p.PaymentCode);
        modelBuilder.Entity<Invoice>().HasIndex(i => i.InvoiceCode);

        #endregion
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {

        foreach (var entry in ChangeTracker.Entries<BasicEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.Id = Guid.NewGuid();
                entry.Entity.CreatedAt = DateTime.UtcNow;
                entry.Entity.IsDeleted = false;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }

            if (entry.State == EntityState.Deleted)
            {
                entry.Entity.DeletedAt = DateTime.UtcNow;
                entry.Entity.IsDeleted = true;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}

