using System.Security.Cryptography;
using System.Text;

namespace MercySocial.Application.Common.Authentication.PasswordHasher;

public class PasswordHasherService : IPasswordHasherService
{
    public string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var hashedBytes = sha256.ComputeHash(passwordBytes);

        return Convert.ToBase64String(hashedBytes);
    }

    public bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        var hashedProvidedPassword = HashPassword(providedPassword);
        return hashedPassword == hashedProvidedPassword;
    }
}