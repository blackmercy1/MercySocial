namespace MercySocial.Presentation.Users.Responses;

public record RegisterResponse(
    string UserName,
    string Email,
    string PasswordHash,
    string? ProfileImageUrl,
    DateTime DateOfBirth,
    DateTime? CreatedAt,
    DateTime? LastLogin,
    string? Bio,
    bool IsActive);