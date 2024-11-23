namespace MercySocial.ApplicationTests.Common.Authentication.JwtTokenGenerator;

public class GenerateJwtTokenShouldReturnValidJwtToken
{
    [Fact]
    public void GenerateJwtToken_ShouldReturnValidJwtToken()
    {
        var user = User.Create(
            id: null,
            userName: "testuser",
            email: "test@example.com",
            passwordHash: "hashedPassword",
            isActive: true
        );
        
        var inMemorySettings = new Dictionary<string, string>
        {
            {"JwtSettings:SecretKey", "039de96d16d0dd3f1f249b83ce310ef20e4276da8ab3e04c68e3e4515fefd880"},
            {"JwtSettings:Issuer", "MockIssuer"},
            {"JwtSettings:Audience", "MockAudience"},
            {"JwtSettings:ExpiryInMinutes", "60"}
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings!)
            .Build();

        var jwtTokenGenerator = new Application.Common.Authentication.JwtTokenGenerator.JwtTokenGeneratorService(configuration);
        
        var token = jwtTokenGenerator.GenerateJwtToken(user);
        
        Assert.NotNull(token);

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        Assert.Equal("testuser", jwtToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value);
        Assert.Equal("test@example.com", jwtToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.Email).Value);
        Assert.Equal("MockIssuer", jwtToken.Issuer);
        Assert.Equal("MockAudience", jwtToken.Audiences.First());
    }
}