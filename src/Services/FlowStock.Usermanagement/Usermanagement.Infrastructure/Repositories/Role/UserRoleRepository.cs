using Microsoft.EntityFrameworkCore;
using Usermanagement.Domain;

namespace Usermanagement.Infrastructure;

public sealed class UserRoleRepository : Repositroy<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(UsermanagementDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<UserRole> GetUserRoleWithParentsByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await Entities
            .Include(u=> u.Role)
            .Include(u=> u.User)
            .FirstOrDefaultAsync(u=> u.Id == userId);
    }
}
