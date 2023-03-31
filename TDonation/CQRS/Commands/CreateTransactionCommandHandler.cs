using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using SharedModels.Enums;
using TDonation.CQRS.ViewModels;
using TDonation.Entities;
using TDonation.Enums;
using TDonation.Services.Interfaces;

namespace TDonation.CQRS.Commands;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, CreateTransactionResponse>
{
    private readonly IZaloPayService _zaloPayService;
    private readonly IDonationService _donationService;


    public CreateTransactionCommandHandler(IZaloPayService zaloPayService, IDonationService donationService)
    {
        _zaloPayService = zaloPayService;
        _donationService = donationService;
    }

    public async Task<CreateTransactionResponse> Handle(CreateTransactionCommand request,
        CancellationToken cancellationToken)
    {
        var createTransactionResponse = request.PaymentServiceEnum switch
        {
            PaymentServiceEnum.ZaloPay => await _zaloPayService.CreateTransactionAsync(request),
            PaymentServiceEnum.Paypal => new CreateTransactionResponse(string.Empty, request.InternalTransactionId, string.Empty),
            _ => throw new NotImplementedException()
        };

        if (request.PaymentServiceEnum == PaymentServiceEnum.ZaloPay)
        {
            if (string.IsNullOrEmpty(createTransactionResponse.PaymentGatewayUrl)) return createTransactionResponse;

            var donationTransactionEntity = new DonationTransactionEntity(request.PostId,
                request.Amount, request.UserId, "", request.InternalTransactionId,
                TransactionTypeEnum.Donation, CurrencyEnum.VND, PaymentServiceEnum.ZaloPay, request.Description,
                TransactionStatusEnum.InProcess, null,
                createTransactionResponse.TransactionToken ?? createTransactionResponse.PaymentGatewayUrl);

            var createInternalTransactionRecordResult =
                await _donationService.CreateTransactionAsync(donationTransactionEntity);
            return !createInternalTransactionRecordResult
                ? new CreateTransactionResponse(string.Empty, string.Empty, "Failure to create transaction")
                : createTransactionResponse;
        }

        return createTransactionResponse;
    }


}