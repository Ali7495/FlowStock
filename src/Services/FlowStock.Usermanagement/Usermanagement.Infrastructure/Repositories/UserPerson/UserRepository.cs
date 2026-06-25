using Microsoft.EntityFrameworkCore;
using Usermanagement.Domain;

namespace Usermanagement.Infrastructure;

public class UserRepository : Repositroy<User>, IUserRepository
{
    public UserRepository(UsermanagementDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<User> GetByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return await Entities.FirstOrDefaultAsync(u=> u.LowerUsername == username, cancellationToken);
    }
}
