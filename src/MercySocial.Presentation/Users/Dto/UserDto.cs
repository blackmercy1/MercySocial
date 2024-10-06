using MercySocial.Domain.UserAggregate.ValueObjects;

namespace MercySocial.Presentation.Users.Dto;

public record UserDto(
    UserId UserId,
    string UserName,
    string Email,
    string PasswordHash,
    string ProfileImageUrl,
    DateTime DateOfBirth,
    DateTime CreatedAt,
    DateTime LastLogin,
    string Bio,
    bool IsActive
);