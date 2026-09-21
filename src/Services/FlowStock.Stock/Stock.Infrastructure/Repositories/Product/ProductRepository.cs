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

    public Task<bool> IsProductCategoryValid(Guid categoryId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
