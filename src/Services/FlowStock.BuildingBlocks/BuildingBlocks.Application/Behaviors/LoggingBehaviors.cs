using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Application;

public sealed class LoggingBehaviors<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<LoggingBehaviors<TRequest,TResponse>> _logger;

    public LoggingBehaviors(ILogger<LoggingBehaviors<TRequest,TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;
        Stopwatch stopwatch = Stopwatch.StartNew();

        _logger.LogInformation("Handling the request {RequestName} ...", requestName);
        

        try
        {
            var response = await next();

            stopwatch.Stop();

            _logger.LogInformation("The request {RequestName} handled in {ElapsedMiliseconds} ms .", requestName, stopwatch.ElapsedMilliseconds);

            return response;
        }
        catch (System.Exception ex)
        {
            stopwatch.Stop();

            _logger.LogError(ex,"Error handled the request {RequestName} in {ElapsedMiliseconds} ms .", requestName,stopwatch.ElapsedMilliseconds);
            
            throw;
        }
    }
}
