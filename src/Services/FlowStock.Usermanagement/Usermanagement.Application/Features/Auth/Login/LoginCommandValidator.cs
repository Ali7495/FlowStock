using FluentValidation;

namespace Usermanagement.Application;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x=> x.username)
        .NotEmpty();

        RuleFor(x=> x.password)
        .NotEmpty();
    }
}
