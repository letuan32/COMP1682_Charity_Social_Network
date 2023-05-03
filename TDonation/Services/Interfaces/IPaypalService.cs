using TDonation.Services.DTOs.Paypal;

namespace TDonation.Services.Interfaces;

public interface IPaypalService
{
    Task<PaypalCaptureResponse> CapturePaymentAsync(string paymentId, int postId);
    Task<CreatePayPalBathPayoutResponse> Payout(CreatePayPalBathPayoutRequest request);

}