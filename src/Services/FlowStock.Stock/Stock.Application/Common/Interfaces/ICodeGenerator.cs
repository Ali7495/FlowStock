namespace Stock.Application;

public interface ICodeGenerator<TCode> where TCode : class
{
    Task<TCode> GenerateCodeAsync(CancellationToken cancellationToken);
}
