using ErrorOr;
using MediatR;
using MercySocial.Application.Users.Repository;
using MercySocial.Domain.common.Errors;
using MercySocial.Domain.UserAggregate;

namespace MercySocial.Application.Users.Commands.CreateUser;

public record CreateUserCommand(
    string UserName,
    string Email,
    string PasswordHash,
    string? ProfileImageUrl,
    DateTime DateOfBirth,
    DateTime? CreatedAt,
    DateTime? LastLogin,
    string? Bio,
    bool IsActive) : IRequest<ErrorOr<User>>;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ErrorOr<User>>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<User>> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByEmailAsync(
            request.Email,
            cancellationToken);

        if (existingUser is not null)
            return Errors.User.EntityExists;

        var user = User.Create(
            userName: request.UserName,
            email: request.Email,
            passwordHash: request.PasswordHash,
            profileImageUrl: request.ProfileImageUrl,
            dateOfBirth: request.DateOfBirth,
            createdAt: request.CreatedAt,
            lastLogin: request.LastLogin,
            bio: request.Bio,
            isActive: request.IsActive
        );

        await _userRepository.AddAsync(user);
        
        return user;
    }
}