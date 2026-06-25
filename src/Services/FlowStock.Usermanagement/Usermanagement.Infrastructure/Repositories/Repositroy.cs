using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Usermanagement.Domain;

namespace Usermanagement.Infrastructure;

public class Repositroy<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly UsermanagementDbContext _dbContext;
    internal DbSet<TEntity> Entities;

    public Repositroy(UsermanagementDbContext dbContext)
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
        return await Entities.AsNoTracking().Where(predicate).ToListAsync(cancellationToken);
    }

    public virtual async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Entities.FindAsync([id], cancellationToken);
    }

}
