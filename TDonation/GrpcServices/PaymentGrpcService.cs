using AutoMapper;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using TDonation.CQRS.Commands;
using TDonation.Services.DTOs.Paypal;
using TDonation.Services.Interfaces;

namespace TDonation.GrpcServices;

public class PaymentGrpcService : Payment.PaymentBase
{
    private readonly ILogger<PaymentGrpcService> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IDonationService _donationService;
    private readonly IPaypalService _paypalService;




    public PaymentGrpcService(ILogger<PaymentGrpcService> logger, IMediator mediator, IMapper mapper, IDonationService donationService, IPaypalService paypalService)
    {
        _logger = logger;
        _mediator = mediator;
        _mapper = mapper;
        _donationService = donationService;
        _paypalService = paypalService;
    }

    public override async Task<DisburseDonationReply> DisburseDonation(DisburseDonationRequest request, ServerCallContext context)
    {
        // var donationAmount = await _donationService.GetDonationAmountByPostId(request.PostId);
        var donationAmount = 1;

        if (donationAmount == 0)
        {
            return new DisburseDonationReply
            {
                ReturnMessage = "No donation found",
                ReturnCode = 404
            };
        }

        var payoutRequest = new CreatePayPalBathPayoutRequest()
        {
            items = new List<PayPalPayoutItem>()
            {
                new PayPalPayoutItem()
                {
                    recipient_type = "EMAIL",
                    amount = new Amount()
                    {
                        currency = "USD",
                        value = 100.ToString()
                    },
                    note = "Donation from TCharity Community",
                    sender_item_id = new Guid().ToString(),
                    receiver = request.UserEmail,
                    recipient_wallet = "PAYPAL"
                }
            },
            sender_batch_header = new SenderBatchHeader()
            {
                sender_batch_id = Guid.NewGuid().ToString(),
                email_message = "Disburse donation from TCharity Community",
                email_subject = "Disburse donation from TCharity Community"
            }
        };

        var createPayoutResponse = await _paypalService.Payout(payoutRequest);
        return new DisburseDonationReply()
        {
            ReturnCode = 200,
            ReturnMessage = createPayoutResponse.batch_header.batch_status
        };
    }

    public override async Task<CreateTransactionReply> CreateTransaction(CreateTransactionRequest request, ServerCallContext context)
    {
        var response = await _mediator.Send(_mapper.Map<CreateTransactionCommand>(request));
        return _mapper.Map<CreateTransactionReply>(response);
    }

    public override async Task<HandleZaloCallbackReply> HandleZaloCallback(HandleZaloCallbackRequest request, ServerCallContext context)
    {
        var response =  await _mediator.Send(_mapper.Map<HandleZaloCallbackCommand>(request));
        
        return _mapper.Map<HandleZaloCallbackReply>(response);
    }
}