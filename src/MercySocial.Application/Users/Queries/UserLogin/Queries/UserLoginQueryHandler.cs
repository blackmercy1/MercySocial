using ErrorOr;
using JetBrains.Annotations;
using MediatR;
using MercySocial.Application.Common.Authentication;
using MercySocial.Application.Common.Authentication.Cookie;
using MercySocial.Application.Common.Authentication.JwtTokenGenerator;
using MercySocial.Application.Common.Authentication.PasswordHasher;
using MercySocial.Application.Users.Repository;
using MercySocial.Domain.Common.Errors;

namespace MercySocial.Application.Users.Queries.UserLogin.Queries;

[UsedImplicitly]
public class UserLoginQueryHandler : IRequestHandler<UserLoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasherService _passwordHasher;
    private readonly IJwtTokenGeneratorService _jwtTokenGeneratorService;

    public UserLoginQueryHandler(
        IUserRepository userRepository,
        IPasswordHasherService passwordHasher,
        IJwtTokenGeneratorService jwtTokenGeneratorService,
        ICookieService cookieService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGeneratorService = jwtTokenGeneratorService;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(
        UserLoginQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        
        if (user is null)
            return Errors.User.EntityWasNotFound;

        if (!_passwordHasher.VerifyPassword(user.PasswordHash, request.Password))
            return Errors.Authentication.InvalidCredentials;

        var token = _jwtTokenGeneratorService.GenerateJwtToken(user);
        
        return new AuthenticationResult(
            user,
            token);
    }
}