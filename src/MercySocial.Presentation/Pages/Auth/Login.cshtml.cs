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
        var userLoginRequest = new UserLoginRequest("null", Email, Password);
    
        using var client = new HttpClient();
        var content = new StringContent(
            JsonSerializer.Serialize(userLoginRequest),
            Encoding.UTF8,
            "application/json");
    
        var response = await client.PostAsync("http://localhost:5155/api/Authentication/Login", content);

        if (response.IsSuccessStatusCode) 
            return RedirectToPage("/Index");

        ErrorMessage = response.ReasonPhrase + response.RequestMessage;
        return Page();
    }
}