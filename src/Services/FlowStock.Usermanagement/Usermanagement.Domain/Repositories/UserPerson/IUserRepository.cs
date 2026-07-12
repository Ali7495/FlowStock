namespace Usermanagement.Domain;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByUsernameAsync(string username, CancellationToken cancellationToken);
    Task<User> GetLoginByNormalizedUsernameAsync(string username, CancellationToken cancellationToken);
}
