using ErrorOr;
using MediatR;
using MercySocial.Domain.UserAggregate;

namespace MercySocial.Application.Users.Commands.CreateUserLogin;

public record CreateUserLoginCommand(
    string UserName,
    string Email,
    string PasswordHash) : IRequest<ErrorOr<User>>;