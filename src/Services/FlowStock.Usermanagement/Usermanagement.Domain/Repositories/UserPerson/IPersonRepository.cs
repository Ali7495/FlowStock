namespace Usermanagement.Domain;

public interface IPersonRepository : IRepository<Person>
{
    Task<Person> GetByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken);
}
