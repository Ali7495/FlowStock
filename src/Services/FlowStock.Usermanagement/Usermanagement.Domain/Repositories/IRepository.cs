using System.Linq.Expressions;

namespace Usermanagement.Domain;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ICollection<TEntity>> FindAsync(Expression<Func<TEntity,bool>> predicate, CancellationToken cancellationToken);
    Task AddAsync(TEntity entity,CancellationToken cancellationToken);
}
