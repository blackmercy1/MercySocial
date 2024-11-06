namespace MercySocial.Presentation.Users.Requests;

public record UserLoginRequest(
    string UserName,
    string Email,
    string Password);