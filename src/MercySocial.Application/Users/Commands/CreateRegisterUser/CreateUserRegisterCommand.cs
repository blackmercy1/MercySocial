using ErrorOr;
using MediatR;
using MercySocial.Domain.UserAggregate;

namespace MercySocial.Application.Users.Commands.CreateRegisterUser;

public record CreateUserRegisterCommand(
    string UserName,
    string Email,
    string Password,
    string? ProfileImageUrl,
    DateTime? DateOfBirth,
    DateTime? CreatedAt,
    DateTime? LastLogin,
    string? Bio,
    bool IsActive) : IRequest<ErrorOr<User>>;