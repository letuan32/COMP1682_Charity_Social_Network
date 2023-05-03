using Newtonsoft.Json;

namespace SharedModels.Paypal;

public class PaypalPaymentCaptureMessage
{
    public string PaymentId { get; set; }
    public int PostId { get; set; }
}