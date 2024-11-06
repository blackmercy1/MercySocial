namespace MercySocial.Presentation.Users.Requests;

public record CreateUserRegisterRequest(
    string UserName,
    string Email,
    string Password,
    string? ProfileImageUrl,
    DateTime? DateOfBirth,
    DateTime? CreatedAt,
    DateTime? LastLogin,
    string? Bio,
    bool IsActive);