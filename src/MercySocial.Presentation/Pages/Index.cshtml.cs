using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MercySocial.Presentation.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public string Content { get; set; }

    public List<PostModel> Posts { get; set; }
    public List<FriendModel> SuggestedFriends { get; set; }

    public void OnGet()
    {
        Posts =
        [
            new()
            {
                UserName = "Alice", UserProfileImageUrl = "/images/user1.jpg", Content = "Hello world!",
                CreatedAt = DateTime.Now.AddMinutes(-30), LikesCount = 12
            },
            new()
            {
                UserName = "Bob", UserProfileImageUrl = "/images/user2.jpg", Content = "Enjoying my day!",
                CreatedAt = DateTime.Now.AddHours(-1), LikesCount = 8
            }
        ];
        
        SuggestedFriends =
        [
            new() {UserName = "Charlie", ProfileImageUrl = "/images/user3.jpg"},
            new() {UserName = "Daisy", ProfileImageUrl = "/images/user4.jpg"}
        ];
    }

    public async Task<IActionResult> OnPostCreatePost()
    {
        if (!string.IsNullOrEmpty(Content))
        {
            Console.WriteLine($"New post created: {Content}");

            // Очистить поле для нового поста после отправки
            Content = string.Empty;
        }

        return RedirectToPage();
    }
}

public class PostModel
{
    public string UserName { get; set; }
    public string UserProfileImageUrl { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public int LikesCount { get; set; }
}

public class FriendModel
{
    public string UserName { get; set; }
    public string ProfileImageUrl { get; set; }
}