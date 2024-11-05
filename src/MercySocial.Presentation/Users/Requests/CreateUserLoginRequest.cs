namespace MercySocial.Presentation.Users.Requests;

public record CreateUserLoginRequest(
    string UserName,
    string Email,
    string Password);