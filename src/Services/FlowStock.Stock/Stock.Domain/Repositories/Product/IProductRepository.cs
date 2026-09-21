namespace Stock.Domain;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetListByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken);
    Task<bool> IsProductCategoryValid(Guid categoryId, CancellationToken cancellationToken);
}
