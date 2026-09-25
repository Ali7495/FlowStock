namespace Stock.Domain;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetListByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken);
}
