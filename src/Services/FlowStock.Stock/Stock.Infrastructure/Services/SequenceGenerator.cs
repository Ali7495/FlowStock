using Microsoft.EntityFrameworkCore;
using Stock.Application;

namespace Stock.Infrastructure;

public sealed class SequenceGenerator : ISequenceGenerator
{
    private readonly StockDbContext _dbContext;

    public SequenceGenerator(StockDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<long> GetNextAsync(
        string sequenceName,
        CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sequenceName);

        FormattableString sql =
            $"""SELECT nextval({sequenceName}::regclass) AS "Value" """;

        return await _dbContext.Database
            .SqlQuery<long>(sql)
            .SingleAsync(cancellationToken);
    }
}
