using FluentValidation;

namespace Usermanagement.Application;

public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x=> x.Username)
        .NotEmpty()
        .MinimumLength(3);

        RuleFor(x=> x.Email)
        .NotEmpty()
        .EmailAddress();

        RuleFor(x=> x.Password)
        .NotEmpty()
        .MinimumLength(4);

        RuleFor(x=> x.Mobile)
        .NotEmpty();
    }
}
