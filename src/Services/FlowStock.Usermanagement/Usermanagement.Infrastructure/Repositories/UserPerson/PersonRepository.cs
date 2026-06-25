using Microsoft.EntityFrameworkCore;
using Usermanagement.Domain;

namespace Usermanagement.Infrastructure;

public class PersonRepository : Repositroy<Person>, IPersonRepository
{
    public PersonRepository(UsermanagementDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Person> GetByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken)
    {
        return await Entities.FirstOrDefaultAsync(p => p.NationalCode == nationalCode, cancellationToken);
    }
}
