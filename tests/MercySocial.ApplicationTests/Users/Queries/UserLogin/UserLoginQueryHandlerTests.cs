using MercySocial.Application.Common.Authentication.Cookie;
using MercySocial.Application.Common.Authentication.JwtTokenGenerator;

namespace MercySocial.ApplicationTests.Users.Queries.UserLogin;

public class UserLoginQueryHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPasswordHasherService> _passwordHasherMock;
    private readonly Mock<IJwtTokenGeneratorService> _jwtTokenGeneratorMock;
    private readonly Mock<ICookieService> _cookieServiceMock;
    private readonly UserLoginQueryHandler _handler;

    public UserLoginQueryHandlerTests()
    {
        _userRepositoryMock = new();
        _passwordHasherMock = new();
        
        _jwtTokenGeneratorMock = new();
        _cookieServiceMock = new Mock<ICookieService>();
        
        _handler = new(
            _userRepositoryMock.Object,
            _passwordHasherMock.Object,
            _jwtTokenGeneratorMock.Object,
            _cookieServiceMock.Object);
    }

    [Fact]
    public async Task Handle_UserNotFound_ReturnsEntityWasNotFoundError()
    {
        var query = new UserLoginQuery("testuser", "nonexistent@example.com", "password123");
        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(query.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User) null);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(Errors.User.EntityWasNotFound);
    }

    [Fact]
    public async Task Handle_InvalidPassword_ReturnsInvalidCredentialsError()
    {
        var query = new UserLoginQuery("testuser", "test@example.com", "wrongpassword");
        var user = User.Create(
            UserId.CreateUniqueUserId(),
            "testuser",
            "test@example.com",
            "hashedPassword123",
            true);

        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(query.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);
        _passwordHasherMock.Setup(hasher => hasher.VerifyPassword(user.PasswordHash, query.Password))
            .Returns(false);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(Errors.Authentication.InvalidCredentials);
    }

    [Fact]
    public async Task Handle_ValidCredentials_ReturnsAuthenticationResult()
    {
        var query = new UserLoginQuery("testuser", "test@example.com", "password123");
        var user = User.Create(
            UserId.CreateUniqueUserId(),
            "testuser",
            "test@example.com",
            "hashedPassword123",
            true);

        var token = "testToken";

        _userRepositoryMock
            .Setup(repo => repo.GetByEmailAsync(query.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        _passwordHasherMock
            .Setup(hasher => hasher.VerifyPassword(user.PasswordHash, query.Password))
            .Returns(true);

        _jwtTokenGeneratorMock
            .Setup(generator => generator.GenerateJwtToken(user))
            .Returns(token);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.IsError.Should().BeFalse();
        result.Value.Token.Should().Be(token);
        result.Value.User.Should().Be(user);
    }
}