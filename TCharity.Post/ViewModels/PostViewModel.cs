namespace TCharity.Post.ViewModels;

public record PostViewModel
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int NumberOfDonation { get; set; }
    public string CreatedById { get; set; }
    public List<string>? ImageUrls { get; set; }
    public List<string>? VideoUrls { get; set; }
    public int NumberOfComment { get; set; }
}