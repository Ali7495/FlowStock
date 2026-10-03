namespace Stock.Domain;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetListByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken);
    Task<Product> GetProductWithChildrenByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Product> GetProductWithCategoryById(Guid id, CancellationToken cancellationToken);
    Task<bool> HasDependenciesAsync(Guid id, CancellationToken cancellationToken);
}
