namespace Usermanagement.Domain;

public interface IUserRoleRepository : IRepository<UserRole>
{
    Task<UserRole> GetUserRoleWithParentsByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}
