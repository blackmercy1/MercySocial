using MercySocial.Presentation.Users.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace MercySocial.Presentation.Pages.Auth;

[IgnoreAntiforgeryToken]
public class LoginModel : PageModel
{
    [BindProperty]
    public string Email { get; set; }

    [BindProperty]
    public string Password { get; set; }
    
    public string ErrorMessage { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        var userLoginRequest = new UserLoginRequest("Something", Email, Password);
    
        using var client = new HttpClient();
        var content = new StringContent(
            JsonSerializer.Serialize(userLoginRequest),
            Encoding.UTF8,
            "application/json");
    
        var response = await client.PostAsync("https://localhost:7181/api/Authentication/Login", content);
        
        if (response.Headers.TryGetValues("Set-Cookie", out var setCookieHeaders))
        {
            var cookieHeader = setCookieHeaders.FirstOrDefault();
            if (!string.IsNullOrEmpty(cookieHeader))
            {
                var cookieParts = cookieHeader.Split(';')[0];
                var cookieKeyValue = cookieParts.Split('=');
                if (cookieKeyValue.Length == 2)
                {
                    var cookieKey = cookieKeyValue[0].Trim();
                    var cookieValue = cookieKeyValue[1].Trim();
                    
                    Response.Cookies.Append(cookieKey, cookieValue, new()
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict
                    });
                }
            }
        }

        if (response.IsSuccessStatusCode)
            return RedirectToPage("/Index");
        
        var errorContent = await response.Content.ReadAsStringAsync();
        
        ErrorMessage = $"Error: {response.ReasonPhrase}\nDetails: {errorContent}";
        return Page();
    }
}