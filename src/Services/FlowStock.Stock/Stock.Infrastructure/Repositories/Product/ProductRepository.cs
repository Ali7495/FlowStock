using Microsoft.EntityFrameworkCore;
using Stock.Domain;

namespace Stock.Infrastructure;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(StockDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Product>> GetListByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        return await Entities.Where(p=> p.ProductCategoryId == categoryId).ToListAsync(cancellationToken);
    }

    public async Task<Product> GetProductWithCategoryById(Guid id, CancellationToken cancellationToken)
    {
        return await Entities.Include(p=> p.ProductCategory).FirstOrDefaultAsync(p=> p.Id == id, cancellationToken);
    }

    public async Task<Product> GetProductWithChildrenByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Entities
                        .Include(p=> p.ProductPrices)
                        .Include(p=> p.InvoiceItems)
                        .Include(p=> p.InventoryReservations)
                        .Include(p=> p.InventoryTransactions)
                        .FirstOrDefaultAsync(p=> p.Id == id, cancellationToken);
    }

    public async Task<bool> HasDependenciesAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Entities.AnyAsync(p=> 
            p.Id == id && (
                p.ProductPrices.Any() ||
                p.InvoiceItems.Any() ||
                p.InventoryReservations.Any() ||
                p.InventoryTransactions.Any()
            ),
            cancellationToken
        );
    }
}
