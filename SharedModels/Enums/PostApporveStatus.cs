using System.ComponentModel;

namespace SharedModels.Enums;

public enum PostApproveStatusEnum
{
    [Description("Pending")]
    Pending = 1,
    [Description("InProcess")]
    InProcess = 2,
    [Description("Approved")]
    Approved = 3,
    [Description("Reject")]
    Reject = 4
}