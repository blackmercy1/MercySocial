using FluentValidation;
using JetBrains.Annotations;

namespace MercySocial.Application.Users.Commands.CreateLogin;

[UsedImplicitly]
public class CreateLoginCommandValidator : AbstractValidator<CreateLoginCommand>
{
    public CreateLoginCommandValidator()
    {
        RuleFor(cmd => cmd.UserName).NotEmpty().WithMessage("Username is required");
        RuleFor(cmd => cmd.Email).NotEmpty().WithMessage("Email is required");
        RuleFor(cmd => cmd.PasswordHash).NotEmpty().WithMessage("Password is required");
    }
}