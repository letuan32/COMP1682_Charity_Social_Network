using SharedModels.Enums;

namespace TPostService.ViewModels;

public class PostRealTimeRecord
{
    public int Id { get; set; }
    public int NumberOfDonation { get; set; }
    public long Amount { get; set; }
    public string ApproveStatus { get; set; }
}