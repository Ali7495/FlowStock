using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Stock.Domain;

namespace Stock.Infrastructure;

public class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository
{
    public ProductCategoryRepository(StockDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<ProductCategory> GetWithProductsById(Guid id, CancellationToken cancellationToken)
    {
        return await Entities.AsNoTracking().Include(p => p.Products).FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<bool> IsCategoryExistByName(string name, CancellationToken cancellationToken)
    {
        return await Entities.AnyAsync(p => p.Name == name, cancellationToken);
    }
}
