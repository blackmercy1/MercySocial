namespace MercySocial.Application.Common.Authentication.PasswordHasher;

public interface IPasswordHasherService
{
    string HashPassword(string password);

    bool VerifyPassword(string hashedPassword, string providedPassword);
}