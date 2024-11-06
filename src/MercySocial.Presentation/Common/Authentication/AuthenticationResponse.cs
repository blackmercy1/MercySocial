namespace MercySocial.Presentation.Common.Authentication;

public record AuthenticationResponse
{
    public string UserName { get; init; }
    public string Email { get; init; }
    public string Token { get; init; }
    
    public AuthenticationResponse() { }
    
    public AuthenticationResponse(string userName, string email, string token)
    {
        UserName = userName;
        Email = email;
        Token = token;
    }
}