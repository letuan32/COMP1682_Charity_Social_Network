using System.Collections.Generic;

namespace TPostService.ViewModels;

public record PostViewModel
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string Location { get; set; }
    public int NumberOfDonation { get; set; }
    public List<string>? ImageUrls { get; set; }
    public List<string>? VideoUrls { get; set; }
    public int NumberOfComment { get; set; }
    public PostAuthorViewModel Author { get; set; }
    public string CreatedById { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Likes { get; set; }

}