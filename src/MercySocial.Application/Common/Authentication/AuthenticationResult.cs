using MercySocial.Domain.UserAggregate;

namespace MercySocial.Application.Common.Authentication;

public record AuthenticationResult(
    User User,
    string Token);