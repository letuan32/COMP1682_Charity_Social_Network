using SharedModels.Enums;

namespace TPostService.ViewModels;

public class PostRealTimeApproveStatus
{
    public PostRealTimeApproveStatus(int id, PostApproveStatusEnum approveStatusEnum, string? lastUpdatedBy = null)
    {
        Id = id;
        ApproveStatus = approveStatusEnum.GetDescription();
        LastUpdatedBy = lastUpdatedBy;
    }
    public int Id { get; set; }
    public string ApproveStatus { get; set; }
    public string? LastUpdatedBy { get; set; }
    public bool IsProcessing { get; set; } = false;
}