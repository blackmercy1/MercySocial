using Microsoft.Extensions.Configuration;

namespace MercySocial.Application.Common.Authentication.PasswordHasher;

public class PasswordHasherService : IPasswordHasherService
{
    private readonly string? _saltKey;

    public PasswordHasherService(IConfiguration configuration)
    {
        _saltKey = configuration["Authentication:SaltKey"];
    }
    
    public string HashPassword(string password) 
        => BCrypt.Net.BCrypt.HashPassword($"{password} + {_saltKey}"); 
    public bool VerifyPassword(string hashedPassword, string providedPassword) 
        => BCrypt.Net.BCrypt.Verify($"{providedPassword} + {_saltKey}", hashedPassword);
}