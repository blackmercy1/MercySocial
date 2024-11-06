using MercySocial.Domain.UserAggregate;

namespace MercySocial.Application.Common.Authentication.JwtTokenGenerator;

public interface IJwtTokenGenerator
{
    string GenerateJwtToken(User user);
}