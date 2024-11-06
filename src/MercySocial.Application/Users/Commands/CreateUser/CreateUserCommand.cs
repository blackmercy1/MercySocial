using ErrorOr;
using MediatR;
using MercySocial.Domain.UserAggregate;

namespace MercySocial.Application.Users.Commands.CreateUser;

public record CreateUserCommand(
    string UserName,
    string Email,
    string PasswordHash,
    string? ProfileImageUrl,
    DateTime? DateOfBirth,
    DateTime? CreatedAt,
    DateTime? LastLogin,
    string? Bio,
    bool IsActive) : IRequest<ErrorOr<User>>;