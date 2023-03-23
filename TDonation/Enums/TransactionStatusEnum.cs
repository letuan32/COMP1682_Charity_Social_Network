using System.ComponentModel;

namespace TDonation.Enums;

public enum TransactionStatusEnum
{
    [Description("In Process")]
    InProcess = 1,
    [Description("Done")]
    Done = 2,
    [Description("Failed")]
    Failed = 3
}