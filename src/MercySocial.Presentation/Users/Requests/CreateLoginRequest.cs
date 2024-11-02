namespace MercySocial.Presentation.Users.Requests;

public record CreateLoginRequest(
    string UserName,
    string Email,
    string PasswordHash);