using System.Linq.Expressions;
using Stock.Domain;

namespace Stock.Infrastructure;

public class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository
{
    public ProductCategoryRepository(StockDbContext dbContext) : base(dbContext)
    {
    }
    

}
