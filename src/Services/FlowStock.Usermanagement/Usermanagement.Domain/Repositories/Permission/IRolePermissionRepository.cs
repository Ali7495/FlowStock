namespace Usermanagement.Domain;

public interface IRolePermissionRepository : IRepository<RolePermission>
{
    Task<List<RolePermission>> GetAllPermissionsWithRoleByRoleIdAsync(Guid roleId, CancellationToken cancellationToken);
}
