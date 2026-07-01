using FluentValidation;
using Usermanagement.Domain;

namespace Usermanagement.Api;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            _logger.LogError(ex, ex.Message);

            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            context.Response.WriteAsJsonAsync(new ErrorResponse(StatusCodes.Status400BadRequest, ex.Message, ex.Errors.Select(e => e.ErrorMessage).ToList()));
        }
        catch (DomainException ex)
        {
            _logger.LogError("Domain",ex);

            context.Response.StatusCode = StatusCodes.Status409Conflict;

            context.Response.WriteAsJsonAsync(new ErrorResponse(StatusCodes.Status409Conflict, ex.Message));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,ex.Message);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            context.Response.WriteAsJsonAsync(new ErrorResponse(StatusCodes.Status500InternalServerError, ex.Message));
        }
    }

}
