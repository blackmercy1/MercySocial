using Microsoft.AspNetCore.Http;

namespace MercySocial.Application.Common.Authentication.Cookie;

public class CookieService : ICookieService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CookieService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void SetTokenCookie(string token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,           
            Secure = true,
            Expires = DateTime.Now.AddMinutes(30),
            SameSite = SameSiteMode.Strict
        };

        _httpContextAccessor.HttpContext?.Response.Cookies.Append("jwt_token", token, cookieOptions);
    }

    public void DeleteTokenCookie()
    {
        _httpContextAccessor.HttpContext?.Response.Cookies.Delete("jwt_token");
    }
}