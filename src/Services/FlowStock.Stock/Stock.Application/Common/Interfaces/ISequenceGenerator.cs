namespace Stock.Application;

public interface ISequenceGenerator
{
    Task<long> GetNextAsync(string sequenceName, CancellationToken cancellationToken);
}
