using Microsoft.EntityFrameworkCore;
using Stock.Domain;

namespace Stock.Infrastructure;

public sealed class ProductPriceRepository : Repository<ProductPrice>, IProductPriceRepository
{
    public ProductPriceRepository(StockDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<ProductPrice> GetActivePriceByProductIdAsync(Guid productId, CancellationToken cancellationToken)
    {
        return await Entities.Include(p=> p.Product).FirstOrDefaultAsync(p=> p.ProductId == productId && p.IsActive, cancellationToken);
    }

    public async Task<List<ProductPrice>> GetAllPricesByProductIdAsync(Guid productId, CancellationToken cancellationToken)
    {
        return await Entities.Where(p=> p.ProductId == productId).Include(p=> p.Product).ToListAsync(cancellationToken);
    }
}
