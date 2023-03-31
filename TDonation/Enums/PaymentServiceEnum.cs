using System.ComponentModel;

namespace TDonation.Enums;

public enum PaymentServiceEnum
{
    [Description("ZaloPay")]
    ZaloPay = 1,
    
    [Description("Paypal")]
    Paypal = 2
}