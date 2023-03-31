using AutoMapper;
using MassTransit;
using Newtonsoft.Json;
using SharedModels.Paypal;
using TDonation.Entities;
using TDonation.Services.Interfaces;

namespace TDonation.Consumers;

public class PaypalCaptureConsumer : IConsumer<PaypalPaymentCaptureMessage>
{
    private readonly IDonationService _donationService;
    private readonly IUserService _userService;
    private readonly IPaypalService _paypalService;
    private readonly IMapper _mapper;
    private readonly ILogger<PaypalCaptureConsumer> _logger;

    public PaypalCaptureConsumer(IDonationService donationService, ILogger<PaypalCaptureConsumer> logger, IUserService userService, IMapper mapper, IPaypalService paypalService)
    {
        _donationService = donationService;
        _logger = logger;
        _userService = userService;
        _mapper = mapper;
        _paypalService = paypalService;
    }

    public async Task Consume(ConsumeContext<PaypalPaymentCaptureMessage> context)
    {

        try
        {
            var paypalResponse = await _paypalService.CapturePaymentAsync(context.Message.PaymentId);
            var donationTransactionEntity = _mapper.Map<DonationTransactionEntity>(paypalResponse);

            var result =
                await _donationService.UpsertTransactionEntityByExternalIdAsync(
                    donationTransactionEntity.InternalTransactionId, donationTransactionEntity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
}