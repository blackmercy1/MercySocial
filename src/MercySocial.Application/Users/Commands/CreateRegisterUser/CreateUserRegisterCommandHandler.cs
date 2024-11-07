using ErrorOr;
using JetBrains.Annotations;
using MediatR;
using MercySocial.Application.Common.Authentication.PasswordHasher;
using MercySocial.Application.Users.Repository;
using MercySocial.Domain.Common.Errors;
using MercySocial.Domain.UserAggregate;

namespace MercySocial.Application.Users.Commands.CreateRegisterUser;

[UsedImplicitly]
public class CreateUserRegisterCommandHandlerUserAlreadyExists : IRequestHandler<CreateUserRegisterCommand, ErrorOr<User>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasherService _passwordHasher;
    
    public CreateUserRegisterCommandHandlerUserAlreadyExists(
        IUserRepository userRepository,
        IPasswordHasherService passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }
    
    public async Task<ErrorOr<User>> Handle(
        CreateUserRegisterCommand request,
        CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetIdByEmailAsync(
            request.Email,
            cancellationToken);
        

        if (existingUser is not null)
            return Errors.User.EntityExists;

        var user = User.Create(
            id: null,
            userName: request.UserName,
            email: request.Email,
            passwordHash: _passwordHasher.HashPassword(request.Password),
            profileImageUrl: request.ProfileImageUrl,
            dateOfBirth: request.DateOfBirth,
            createdAt: request.CreatedAt,
            lastLogin: request.LastLogin,
            bio: request.Bio,
            isActive: request.IsActive
        );

        await _userRepository.AddAsync(user, cancellationToken);
        
        return user;
    }
}