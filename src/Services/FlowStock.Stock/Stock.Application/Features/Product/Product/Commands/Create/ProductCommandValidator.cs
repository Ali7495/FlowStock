using FluentValidation;

namespace Stock.Application;

public sealed class ProductCommandValidator : AbstractValidator<ProductCommand>
{
    public ProductCommandValidator()
    {
        RuleFor(x => x.productName)
        .NotEmpty()
        .NotNull();

        RuleFor(x => x.categoryId)
        .NotEmpty()
        .NotNull();
    }
}
