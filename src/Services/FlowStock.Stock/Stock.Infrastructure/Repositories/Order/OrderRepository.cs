using Microsoft.EntityFrameworkCore;
using Stock.Domain;

namespace Stock.Infrastructure;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(StockDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Order> GetByCodeAsync(string code, CancellationToken cancellationToken)
    {
        return await Entities.FirstOrDefaultAsync(o=> o.Code.Value == code, cancellationToken);
    }

    public async Task<List<Order>> GetListByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken)
    {
        return await Entities.Where(o=> o.CustomerId == customerId).ToListAsync(cancellationToken);
    }
}
