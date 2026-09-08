namespace Stock.Domain;

public interface IProductCategoryRepository : IRepository<ProductCategory>
{
    Task<bool> IsCategoryExistByName(string name, CancellationToken cancellationToken);
    Task<ProductCategory> GetWithProductsById(Guid id, CancellationToken cancellationToken);
}
