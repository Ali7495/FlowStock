using FluentValidation;
using MediatR;

namespace Stock.Application;

public class ProductPriceCommandValidator : AbstractValidator<ProductPriceCommand>
{
    public ProductPriceCommandValidator()
    {
        RuleFor(x=> x.price)
        .NotEmpty()
        .GreaterThanOrEqualTo(0);
    }
}
