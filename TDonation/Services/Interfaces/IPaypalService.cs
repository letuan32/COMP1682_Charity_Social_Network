using TDonation.Services.DTOs.Paypal;

namespace TDonation.Services.Interfaces;

public interface IPaypalService
{
    Task<PaypalCaptureResponse> CapturePaymentAsync(string paymentId);
}