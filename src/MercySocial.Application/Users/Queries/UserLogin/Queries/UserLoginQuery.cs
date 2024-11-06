using ErrorOr;
using MediatR;
using MercySocial.Application.Common.Authentication;

namespace MercySocial.Application.Users.Queries.UserLogin.Queries;

public record UserLoginQuery(
    string UserName,
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;