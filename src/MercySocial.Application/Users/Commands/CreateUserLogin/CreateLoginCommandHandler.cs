using ErrorOr;
using JetBrains.Annotations;
using MediatR;
using MercySocial.Application.Users.Repository;
using MercySocial.Domain.Common.Errors;
using MercySocial.Domain.UserAggregate;

namespace MercySocial.Application.Users.Commands.CreateUserLogin;

[UsedImplicitly]
public class CreateLoginCommandHandler : IRequestHandler<CreateUserLoginCommand, ErrorOr<User>>
{
    private readonly IUserRepository _userRepository;

    public CreateLoginCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<User>> Handle(
        CreateUserLoginCommand request,
        CancellationToken cancellationToken)
    {
        var userId = await _userRepository.GetIdByEmailAsync(
            request.Email,
            cancellationToken);

        if (userId is null)
            return Errors.User.EntityWasNotFound;
        
        var user = User.Create(
            id: userId,
            userName: request.UserName,
            email: request.Email,
            passwordHash: request.PasswordHash,
            isActive: true);
        
        await _userRepository.AddAsync(user, cancellationToken);
        
        return user;
    }
}