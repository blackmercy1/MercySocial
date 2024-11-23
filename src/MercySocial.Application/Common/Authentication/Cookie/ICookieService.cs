namespace MercySocial.Application.Common.Authentication.Cookie;

public interface ICookieService
{
    void SetTokenCookie(string token);
    void DeleteTokenCookie();
}