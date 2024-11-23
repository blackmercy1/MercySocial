using MercySocial.Domain.UserAggregate;

namespace MercySocial.Application.Common.Authentication.JwtTokenGenerator;

public interface IJwtTokenGeneratorService
{
    string GenerateJwtToken(User user);
}