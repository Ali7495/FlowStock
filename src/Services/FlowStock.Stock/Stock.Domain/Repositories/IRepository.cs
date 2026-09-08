using System.Linq.Expressions;

namespace Stock.Domain;

public interface IRepository<TEntity> where TEntity : class
{
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task<ICollection<TEntity>> FindAsync(Expression<Func<TEntity,bool>> predicate, CancellationToken cancellationToken);
    Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}
