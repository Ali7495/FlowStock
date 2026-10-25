using FluentValidation;

namespace Stock.Application;

public sealed class UpdateProductPriceValidator : AbstractValidator<UpdateProductPriceCommand>
{
    public UpdateProductPriceValidator()
    {
        RuleFor(x=> x.price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Can not set price less than 0.");
    }
}
