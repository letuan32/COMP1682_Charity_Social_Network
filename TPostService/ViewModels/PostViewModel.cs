using System.Collections.Generic;

namespace TPostService.ViewModels;

public record PostViewModel
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string Location { get; set; }
    public int NumberOfDonation { get; set; }
    public int Views { get; set; }
    public List<string> MediaUrls { get; set; } = new List<string>();
    public List<string> DocumentUrls { get; set; } = new List<string>();
    public int NumberOfComment { get; set; }
    public long ExpectedAmount { get; set; }
    public PostAuthorViewModel Author { get; set; }
    public string CreatedById { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpectedReceivedDate { get; set; }
    public string Currency { get; set; }
    public string Category { get; set; }
    public string ApproveStatus { get; set; }
}