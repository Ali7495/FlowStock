namespace Stock.Application;

public interface ICurrentUser
{
    bool IsAuthenticated { get; }
    Guid PersonId { get; }
    Guid UserId { get; }
    string Username { get; }
    IReadOnlyCollection<string> Roles { get; }
    bool IsInRole(string role);
}
