namespace Stock.Domain;

public interface IOrderRepository : IRepository<Order>
{
    Task<List<Order>> GetListByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken);
    Task<Order> GetByCodeAsync(string code, CancellationToken cancellationToken);
}
