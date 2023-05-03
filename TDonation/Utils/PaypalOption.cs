namespace TDonation.Utils;

public class PaypalOption
{
    public string ClientId { get; set; } = String.Empty;
    public string ClientSecret { get; set; } = String.Empty;
    public string GetPaymentCaptureUrl { get; set; } = String.Empty;
    public string CreatePayoutUrl { get; set; } = String.Empty;


}