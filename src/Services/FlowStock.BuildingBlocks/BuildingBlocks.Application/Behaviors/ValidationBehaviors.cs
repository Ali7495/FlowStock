using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace BuildingBlocks.Application;

public class ValidationBehaviors<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{

    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviors(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            ValidationContext<TRequest> context = new(request);

            ValidationResult[] results = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken))
            );

            List<ValidationFailure> failures = results.SelectMany(r=> r.Errors).Where(x=> x is not null).ToList();

            if (failures.Any())
                throw new ValidationException(failures);
        }

        return await next();
    }
}
