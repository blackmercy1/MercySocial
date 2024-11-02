using FluentValidation;
using JetBrains.Annotations;

namespace MercySocial.Application.Users.Commands.CreateUser;

[UsedImplicitly]
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(50).WithMessage("Username must not exceed 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters.");

        RuleFor(x => x.PasswordHash)
            .NotEmpty().WithMessage("Password hash is required.");

        RuleFor(x => x.ProfileImageUrl)
            .MaximumLength(255).WithMessage("Profile image URL must not exceed 255 characters.")
            .When(x => !string.IsNullOrEmpty(x.ProfileImageUrl));

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required.")
            .LessThan(DateTime.UtcNow).WithMessage("Date of birth must be in the past.");

        RuleFor(x => x.Bio)
            .MaximumLength(500).WithMessage("Bio must not exceed 500 characters.");

        RuleFor(x => x.IsActive)
            .NotNull().WithMessage("IsActive status is required.");
    }
}