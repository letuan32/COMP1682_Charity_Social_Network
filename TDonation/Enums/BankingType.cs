using System.ComponentModel;

namespace TDonation.Enums;

public enum BankingTypeEnum
{
    [Description("ATM")]
    ATM = 1,
    [Description("Credit Card")]
    Visa = 2,
    [Description("eWallet")]
    EWallet=3
}