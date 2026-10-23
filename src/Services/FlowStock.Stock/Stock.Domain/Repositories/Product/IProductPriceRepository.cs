namespace Stock.Domain;

public interface IProductPriceRepository
{
    Task<ProductPrice> GetActivePriceByProductIdAsync(Guid productId, CancellationToken cancellationToken);
    Task<List<ProductPrice>> GetAllPricesByProductIdAsync(Guid productId, CancellationToken cancellationToken);
}
