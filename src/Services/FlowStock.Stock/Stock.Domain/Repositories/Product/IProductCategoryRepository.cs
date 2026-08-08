namespace Stock.Domain;

public interface IProductCategoryRepository : IRepository<ProductCategory>
{
    Task<bool> IsCategoryExistByName(string name, CancellationToken cancellationToken);
}
