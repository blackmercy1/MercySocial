using MercySocial.Domain.common;
using MercySocial.Domain.UserAggregate.ValueObjects;

namespace MercySocial.Domain.UserAggregate;

public class User : AggregateRoot<UserId>
{
    public string UserName { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string? ProfileImageUrl { get; private set; }

    public DateTime DateOfBirth { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public DateTime? LastLogin { get; private set; }
    public string? Bio { get; private set; }

    public bool IsActive { get; private set; }

    private User(
        UserId id,
        string userName,
        string email,
        string passwordHash,
        string? profileImageUrl,
        DateTime dateOfBirth,
        DateTime? createdAt,
        DateTime? lastLogin,
        string? bio,
        bool isActive)
        : base(id)
    {
        UserName = userName;
        Email = email;
        PasswordHash = passwordHash;
        ProfileImageUrl = profileImageUrl;
        DateOfBirth = dateOfBirth;
        CreatedAt = createdAt ?? DateTime.UtcNow;
        LastLogin = lastLogin;
        Bio = bio;
        IsActive = isActive;
    }

    public static User Create(
        string userName,
        string email,
        string passwordHash,
        string? profileImageUrl,
        DateTime dateOfBirth,
        DateTime? createdAt,
        DateTime? lastLogin,
        string? bio,
        bool isActive)
    {
        return new(
            id: UserId.CreateUniqueUserId(),
            userName: userName,
            email: email,
            passwordHash: passwordHash,
            profileImageUrl: profileImageUrl,
            dateOfBirth: dateOfBirth,
            createdAt: createdAt,
            lastLogin: lastLogin,
            bio: bio,
            isActive: isActive
        );
    }

#pragma warning disable CS8618
    private User() { }
#pragma warning restore CS8618
}