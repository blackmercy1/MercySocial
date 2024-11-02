using MediatR;
using ErrorOr;
using MercySocial.Domain.UserAggregate;

namespace MercySocial.Application.Users.Commands.CreateLogin;

public record CreateLoginCommand(
    string UserName,
    string Email,
    string PasswordHash) : IRequest<ErrorOr<User>>;