using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Stock.Domain;

namespace Stock.Infrastructure;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly StockDbContext _dbContext;
    internal DbSet<TEntity> Entities;

    public Repository(StockDbContext dbContext)
    {
        _dbContext = dbContext;
        Entities = _dbContext.Set<TEntity>();
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await Entities.AddAsync(entity, cancellationToken);
    }

    public virtual async Task<ICollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        return await Entities.Where(predicate).ToListAsync(cancellationToken);
    }

    public virtual async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await Entities.ToListAsync(cancellationToken);
    }

    public virtual async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Entities.FindAsync([id], cancellationToken);
    }

    public void Update(TEntity entity)
    {
        Entities.Update(entity);
    }
}
