using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace BuildingBlocks.Application;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (System.Exception exception)
        {
            _logger.LogError(exception, "Unhandled exception occured. TraceId is {TraceId}", context.TraceIdentifier);

            await HandleException(context,exception);
        }
    }

    private static async Task HandleException(HttpContext context, Exception exception)
    {
        int statusCode = exception switch
        {
            ArgumentException => StatusCodes.Status400BadRequest,
            KeyNotFoundException => StatusCodes.Status404NotFound,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        MiddlewareResponseObject middlewareResponseObject = new()
        {
            StatusCode = statusCode,
            Message = GetMessage(exception)
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(middlewareResponseObject));
    }

    private static string GetMessage(Exception ex)
    {
        return ex switch
        {
            ArgumentException => ex.Message,
            KeyNotFoundException => ex.Message,
            UnauthorizedAccessException => "You are not Authorized!",
            _ => "An unexpected error occured!"
        };
    }

}
