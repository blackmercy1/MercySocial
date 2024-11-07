namespace MercySocial.ApplicationTests.Users.Commands.CreateUserRegister;

public class CreateUserRegisterCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPasswordHasherService> _passwordHasherMock;
    private readonly CreateUserRegisterCommandHandlerUserAlreadyExists _handler;

    public CreateUserRegisterCommandHandlerTests()
    {
        _userRepositoryMock = new();
        _passwordHasherMock = new();
        _handler = new(
            _userRepositoryMock.Object, 
            _passwordHasherMock.Object);
    }

    [Fact]
    public async Task Handle_UserAlreadyExists_ReturnsEntityExistsError()
    {
        var command = new CreateUserRegisterCommand(
            UserName: "ExistingUser",
            Email: "existinguser@example.com",
            Password: "Password123",
            ProfileImageUrl: "http://example.com/image.png",
            DateOfBirth: DateTime.UtcNow.AddYears(-25),
            CreatedAt: DateTime.UtcNow,
            LastLogin: DateTime.UtcNow,
            Bio: "Bio",
            IsActive: true
        );

        var userId = UserId.CreateUniqueUserId();

        _userRepositoryMock
            .Setup(repo => repo.GetIdByEmailAsync(command.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(userId); 
        
        var result = await _handler.Handle(command, CancellationToken.None);
        
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(Errors.User.EntityExists);
    }

    [Fact]
    public async Task Handle_UserDoesNotExist_CreatesUserSuccessfully()
    {
        var command = new CreateUserRegisterCommand(
            UserName: "NewUser",
            Email: "newuser@example.com",
            Password: _passwordHasherMock.Object.HashPassword("hashedPassword"),
            ProfileImageUrl: "http://example.com/image.png",
            DateOfBirth: DateTime.UtcNow.AddYears(-25),
            CreatedAt: DateTime.UtcNow,
            LastLogin: DateTime.UtcNow,
            Bio: "Bio",
            IsActive: true
        );
        
        _userRepositoryMock
            .Setup(repo => repo.GetIdByEmailAsync(command.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync((UserId?)null); 
        
        var result = await _handler.Handle(command, CancellationToken.None);
        
        _userRepositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .Returns(() => Task.FromResult(null as User)!);
        
        result.IsError.Should().BeFalse();
        result.Value.Email.Should().Be(command.Email);
    }
}