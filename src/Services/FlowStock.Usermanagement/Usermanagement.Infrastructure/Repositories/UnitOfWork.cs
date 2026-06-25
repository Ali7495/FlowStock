using Usermanagement.Domain;

namespace Usermanagement.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly UsermanagementDbContext _dbContext;

    public UnitOfWork(UsermanagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
