using ErrorOr;
using JetBrains.Annotations;
using MediatR;
using MercySocial.Application.Common.Authentication.PasswordHasher;
using MercySocial.Application.Users.Repository;
using MercySocial.Domain.Common.Errors;
using MercySocial.Domain.UserAggregate;

namespace MercySocial.Application.Users.Commands.CreateUserLogin;

[UsedImplicitly]
public class CreateLoginCommandHandler : IRequestHandler<CreateUserLoginCommand, ErrorOr<User>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasherService _passwordHasher;

    public CreateLoginCommandHandler(
        IUserRepository userRepository,
        IPasswordHasherService passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<ErrorOr<User>> Handle(
        CreateUserLoginCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        return user is null
            ? Errors.User.EntityWasNotFound
            : !_passwordHasher.VerifyPassword(user.PasswordHash, request.Password)
                ? Errors.Authentication.InvalidCredentials
                : user;
    }
}