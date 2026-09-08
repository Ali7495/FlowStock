using System.Diagnostics;
using MediatR;

namespace BuildingBlocks.Application;

public sealed class TracingBehaviors<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private static readonly ActivitySource activitySource = new("FlowStock.Stock.Application");
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        using Activity activity = activitySource.StartActivity(typeof(TRequest).Name);

        activity?.SetTag("mediatr.request",typeof(TRequest).Name);

        try
        {
            return await next();
        }
        catch (System.Exception ex)
        {
            activity?.SetTag("error.type",ex.GetType().Name);
            throw;
        }
    }
}
