using FluentValidation;
using JetBrains.Annotations;

namespace MercySocial.Application.Users.Queries.UserLogin.Queries;

[UsedImplicitly]
public class UserLoginQueryValidator : AbstractValidator<UserLoginQuery>
{
    public UserLoginQueryValidator()
    {
        RuleFor(cmd => cmd.UserName).NotEmpty().WithMessage("Username is required");
        RuleFor(cmd => cmd.Email).NotEmpty().WithMessage("Email is required");
        RuleFor(cmd => cmd.Password).NotEmpty().WithMessage("Password is required");
    }
}