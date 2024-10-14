using MercySocial.Domain.common;
using MercySocial.Domain.UserAggregate.ValueObjects;

namespace MercySocial.Domain.UserAggregate;

public class User : AggregateRoot<UserId>
{
    protected User(
        UserId id,
        string userName,
        string email,
        string passwordHash,
        string profileImageUrl,
        DateTime dateOfBirth,
        DateTime createdAt,
        DateTime lastLogin,
        string bio,
        bool isActive) 
        : base(id)
    {
        UserName = userName;
        Email = email;
        PasswordHash = passwordHash;
        ProfileImageUrl = profileImageUrl;
        DateOfBirth = dateOfBirth;
        CreatedAt = createdAt;
        LastLogin = lastLogin;
        Bio = bio;
        IsActive = isActive;
    }

    public string UserName { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string ProfileImageUrl { get; private set; }
    
    public DateTime DateOfBirth { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime LastLogin { get; private set; }
    
    public string Bio { get; private set; }
    public bool IsActive { get; private set; }

    private User() { }
}