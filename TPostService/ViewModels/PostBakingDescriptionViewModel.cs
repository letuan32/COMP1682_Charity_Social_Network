using System.ComponentModel.DataAnnotations;

namespace TPostService.ViewModels;

public class PostBakingDescriptionViewModel
{
    public int PostId { get; set; }
    public string Description { get; set; } = string.Empty;
}