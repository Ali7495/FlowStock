using Stock.Domain;

namespace Stock.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly StockDbContext _dbContext;

    public UnitOfWork(StockDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
