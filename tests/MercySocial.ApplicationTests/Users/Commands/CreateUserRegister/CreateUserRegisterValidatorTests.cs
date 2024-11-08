namespace MercySocial.ApplicationTests.Users.Commands.CreateUserRegister;

public class CreateUserRegisterValidatorTests
{
    private readonly CreateUserRegisterValidator _validator;

    public CreateUserRegisterValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Should_Have_Error_When_UserName_Is_Empty()
    {
        var command = new CreateUserRegisterCommand(
            UserName: "",
            Email: "test@example.com",
            Password: "Password123",
            ProfileImageUrl: null,
            DateOfBirth: new DateTime(1990, 1, 1),
            CreatedAt: DateTime.UtcNow,
            LastLogin: DateTime.UtcNow,
            Bio: "Bio",
            IsActive: true);

        var result = _validator.TestValidate(command);
        
        result
            .ShouldHaveValidationErrorFor(x => x.UserName)
            .WithErrorMessage("Username is required.");
    }

    [Fact]
    public void Should_Have_Error_When_UserName_Exceeds_MaxLength()
    {
        var command = new CreateUserRegisterCommand(
            UserName: new('a', 51),
            Email: "test@example.com",
            Password: "Password123",
            ProfileImageUrl: null,
            DateOfBirth: new DateTime(1990, 1, 1),
            CreatedAt: DateTime.UtcNow,
            LastLogin: DateTime.UtcNow,
            Bio: "Bio",
            IsActive: true);

        var result = _validator.TestValidate(command);
        
        result
            .ShouldHaveValidationErrorFor(x => x.UserName)
            .WithErrorMessage("Username must not exceed 50 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Empty()
    {
        var command = new CreateUserRegisterCommand(
            UserName: "testuser",
            Email: "",
            Password: "Password123",
            ProfileImageUrl: null,
            DateOfBirth: new DateTime(1990, 1, 1),
            CreatedAt: DateTime.UtcNow,
            LastLogin: DateTime.UtcNow,
            Bio: "Bio",
            IsActive: true);

        var result = _validator.TestValidate(command);
        
        result
            .ShouldHaveValidationErrorFor(x => x.Email)
            .WithErrorMessage("Email is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Email_Format_Is_Invalid()
    {
        var command = new CreateUserRegisterCommand(
            UserName: "testuser",
            Email: "invalid-email",
            Password: "Password123", 
            ProfileImageUrl: null,
            DateOfBirth: new DateTime(1990, 1, 1),
            CreatedAt: DateTime.UtcNow,
            LastLogin: DateTime.UtcNow, 
            Bio: "Bio",
            IsActive: true);

        var result = _validator.TestValidate(command);
        
        result
            .ShouldHaveValidationErrorFor(x => x.Email)
            .WithErrorMessage("Invalid email format.");
    }

    [Fact]
    public void Should_Have_Error_When_Password_Is_Empty()
    {
        var command = new CreateUserRegisterCommand(
            UserName: "testuser",
            Email: "test@example.com",
            Password: "",
            ProfileImageUrl: null,
            DateOfBirth: new DateTime(1990, 1, 1),
            CreatedAt: DateTime.UtcNow,
            LastLogin: DateTime.UtcNow, 
            Bio: "Bio",
            IsActive: true);

        var result = _validator.TestValidate(command);
        
        result
            .ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorMessage("Password hash is required.");
    }
}