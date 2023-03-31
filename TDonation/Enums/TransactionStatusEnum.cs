using System.ComponentModel;

namespace TDonation.Enums;

public enum TransactionStatusEnum
{
    [Description("In Process")]
    InProcess = 1,
    [Description("Success")]
    Success = 2,
    [Description("Failed")]
    Failed = 3
}