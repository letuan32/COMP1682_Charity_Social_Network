using SharedModels.Enums;

namespace SharedModels.Post;

public class UpdatePostApproveStatusMessage
{
    public int PostId { get; set; }
    public string Message { get; set; } = string.Empty;
    public PostApproveStatusEnum PostApproveStatusEnum { get; set; }
}