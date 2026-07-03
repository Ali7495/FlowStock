using Microsoft.EntityFrameworkCore;
using Usermanagement.Domain;

namespace Usermanagement.Infrastructure;

public class UserRepository : Repositroy<User>, IUserRepository
{
    public UserRepository(UsermanagementDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Entities
        .Include(u=> u.Person)
        .FirstOrDefaultAsync(u=> u.Id == id, cancellationToken);
    }

    public async Task<User> GetByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return await Entities.FirstOrDefaultAsync(u=> u.NormalizedUsername == username.Trim().ToLower(), cancellationToken);
    }
}
