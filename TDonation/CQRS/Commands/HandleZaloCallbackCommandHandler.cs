using MediatR;
using TDonation.CQRS.ViewModels;
using TDonation.Entities;
using TDonation.Enums;
using TDonation.Services;
using TDonation.Services.Interfaces;

namespace TDonation.CQRS.Commands;

public class HandleZaloCallbackCommandHandler : IRequestHandler<HandleZaloCallbackCommand, HandleZaloCallbackResponse>
{
    private readonly IZaloPayService _zaloPayService;
    private readonly IDonationService _donationService;

    public HandleZaloCallbackCommandHandler(IZaloPayService zaloPayService, IDonationService donationService)
    {
        _zaloPayService = zaloPayService;
        _donationService = donationService;
    }


    public async Task<HandleZaloCallbackResponse> Handle(HandleZaloCallbackCommand request, CancellationToken cancellationToken)
    {
        var handleResult = await _zaloPayService.HandZaloCallbackAsync(request, cancellationToken);
        if (handleResult.ReturnCode == 1)
        {
            await _donationService.UpdateTransactionStatusAsync(request.ParsedData.AppTransId,
                TransactionStatusEnum.Success);
        }
        else
        {
            await _donationService.UpdateTransactionStatusAsync(request.ParsedData.AppTransId,
                TransactionStatusEnum.Failed);
        }

        return handleResult;

    }
}