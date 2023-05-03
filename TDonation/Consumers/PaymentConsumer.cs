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
    private readonly IFirebaseService _firebaseService;

    private readonly ILogger<PaypalCaptureConsumer> _logger;

    public PaypalCaptureConsumer(IDonationService donationService, ILogger<PaypalCaptureConsumer> logger, IUserService userService, IMapper mapper, IPaypalService paypalService, IFirebaseService firebaseService)
    {
        _donationService = donationService;
        _logger = logger;
        _userService = userService;
        _mapper = mapper;
        _paypalService = paypalService;
        _firebaseService = firebaseService;
    }

    public async Task Consume(ConsumeContext<PaypalPaymentCaptureMessage> context)
    {

        try
        {
            var paypalResponse = await _paypalService.CapturePaymentAsync(context.Message.PaymentId, context.Message.PostId);
            var donationTransactionEntity = _mapper.Map<DonationTransactionEntity>(paypalResponse);
            donationTransactionEntity.PostId = context.Message.PostId;
            
            
            var result =
                await _donationService.UpsertTransactionEntityByExternalIdAsync(
                    donationTransactionEntity.InternalTransactionId, donationTransactionEntity);
            
                var totalAmount = await _donationService.GetDonationAmountByPostId(context.Message.PostId);
                await _firebaseService.NewDonationEvent(context.Message.PostId, totalAmount);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
}