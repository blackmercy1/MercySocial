using MercySocial.Presentation.Users.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace MercySocial.Presentation.Pages.Auth;

[IgnoreAntiforgeryToken]
public class RegisterModel : PageModel
{
    [BindProperty]
    public string Email { get; set; }
    
    [BindProperty]
    public string Password { get; set; }
    
    public string ErrorMessage { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        var registerRequest = new CreateUserRegisterRequest("private void hello gyus", Email, Password, null, null, null, null, null, true);

        using var client = new HttpClient();
        var content = new StringContent(JsonSerializer.Serialize(registerRequest), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("https://localhost:7181/api/authentication/register", content);

        if (!response.IsSuccessStatusCode)
        {
            ErrorMessage = "Registration failed. Please try again.";
            return Page();
        }

        return RedirectToPage("/Index");
    }
}