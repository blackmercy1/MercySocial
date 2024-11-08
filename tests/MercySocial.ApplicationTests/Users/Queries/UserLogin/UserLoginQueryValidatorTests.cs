namespace MercySocial.ApplicationTests.Users.Queries.UserLogin;

public class UserLoginQueryValidatorTests
{
    private readonly UserLoginQueryValidator _validator;

    public UserLoginQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Should_Have_Error_When_UserName_Is_Empty()
    {
        var query = new UserLoginQuery(
            UserName: "",
            Email: "test@example.com",
            Password: "SecurePassword123"
        );

        var result = _validator.TestValidate(query);

        result
            .ShouldHaveValidationErrorFor(x => x.UserName)
            .WithErrorMessage("Username is required");
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Empty()
    {
        var query = new UserLoginQuery(
            UserName: "testuser",
            Email: "",
            Password: "SecurePassword123"
        );

        var result = _validator.TestValidate(query);

        result
            .ShouldHaveValidationErrorFor(x => x.Email)
            .WithErrorMessage("Email is required");
    }

    [Fact]
    public void Should_Have_Error_When_Password_Is_Empty()
    {
        var query = new UserLoginQuery(
            UserName: "testuser",
            Email: "test@example.com",
            Password: ""
        );

        var result = _validator.TestValidate(query);

        result
            .ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorMessage("Password is required");
    }

    [Fact]
    public void Should_Not_Have_Error_When_All_Fields_Are_Valid()
    {
        var query = new UserLoginQuery(
            UserName: "testuser",
            Email: "test@example.com",
            Password: "SecurePassword123"
        );

        var result = _validator.TestValidate(query);

        result.ShouldNotHaveValidationErrorFor(x => x.UserName);
        result.ShouldNotHaveValidationErrorFor(x => x.Email);
        result.ShouldNotHaveValidationErrorFor(x => x.Password);
    }
}