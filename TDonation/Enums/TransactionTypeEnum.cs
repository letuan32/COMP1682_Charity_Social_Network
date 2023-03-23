using System.ComponentModel;

namespace TDonation.Enums;

public enum TransactionTypeEnum
{
    [Description("Donation")]
    Donation = 1,
    [Description("Disbursement")]
    Disbursement = 2
}