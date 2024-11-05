namespace MercySocial.Application.Common.Authentication.PasswordHasher;

public class PasswordHasherService : IPasswordHasherService
{
    public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
    public bool VerifyPassword(string hashedPassword, string providedPassword) 
        => hashedPassword == BCrypt.Net.BCrypt.HashPassword(providedPassword);
}