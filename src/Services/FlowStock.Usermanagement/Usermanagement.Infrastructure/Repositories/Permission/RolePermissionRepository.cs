using Microsoft.EntityFrameworkCore;
using Usermanagement.Domain;

namespace Usermanagement.Infrastructure;

public class RolePermissionRepository : Repositroy<RolePermission>, IRolePermissionRepository
{
    public RolePermissionRepository(UsermanagementDbContext dbContext) : base(dbContext)
    {
    }

    public Task<List<RolePermission>> GetAllPermissionsWithRoleByRoleIdAsync(Guid roleId, CancellationToken cancellationToken)
    {
        return Entities.Where(r=> r.RoleId == roleId).ToListAsync(cancellationToken);
    }
}
