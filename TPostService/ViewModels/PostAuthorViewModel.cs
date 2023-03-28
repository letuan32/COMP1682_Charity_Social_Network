namespace TPostService.ViewModels;

public class PostAuthorViewModel
{
    public PostAuthorViewModel(string email, string displayName, string avatarUrl)
    {
        Email = email;
        DisplayName = displayName;
        AvatarUrl = avatarUrl;
    }

    public PostAuthorViewModel()
    {
        
    }

    public string Email { get; set; }
    public string DisplayName { get; set; }
    public string AvatarUrl { get; set; }
}