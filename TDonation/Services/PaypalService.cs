using System.Text;
using AutoMapper;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using TDonation.Services.DTOs.Paypal;
using TDonation.Services.Interfaces;
using TDonation.Utils;

namespace TDonation.Services;

public class PaypalService : IPaypalService
{
    private readonly PaypalOption _paypalOption;
    private readonly ILogger<PaypalService> _logger;
    private readonly IMapper _mapper;

    public PaypalService(IOptions<PaypalOption> paypalOption, ILogger<PaypalService> logger, IMapper mapper)
    {
        _paypalOption = paypalOption.Value;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<PaypalCaptureResponse> CapturePaymentAsync(string paymentId, int postId)
    {
        var base64Data = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_paypalOption.ClientId}:{_paypalOption.ClientSecret}")); // credentials
        
        _logger.LogInformation("Send request to get payment capture. PaymentId: {PaymentId}", paymentId);
        return await _paypalOption.GetPaymentCaptureUrl
            .WithHeader("Authorization", $"Basic {base64Data}")
            .AppendPathSegment(paymentId)
            .GetJsonAsync<PaypalCaptureResponse>();
    }

    public async Task<CreatePayPalBathPayoutResponse> Payout(CreatePayPalBathPayoutRequest request)
    {
        var base64Data = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_paypalOption.ClientId}:{_paypalOption.ClientSecret}")); // credentials

        var response = await _paypalOption.CreatePayoutUrl
            .WithHeader("Authorization", $"Basic {base64Data}")
            .PostJsonAsync(request)
            .ReceiveJson<CreatePayPalBathPayoutResponse>();

        return response;
    }
}