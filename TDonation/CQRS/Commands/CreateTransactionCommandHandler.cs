using MediatR;
using TDonation.CQRS.ViewModels;
using TDonation.Helpers;
using TDonation.Services;
using TDonation.Services.Interfaces;

namespace TDonation.CQRS.Commands;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, CreateTransactionResponse>
{
    private readonly IZaloPayService _zaloPayService;


    public CreateTransactionCommandHandler(IZaloPayService zaloPayService)
    {
        _zaloPayService = zaloPayService;
    }

    public async Task<CreateTransactionResponse> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        return request.PaymentServiceEnum switch
        {
            PaymentServiceEnum.ZaloPay => await _zaloPayService.CreateTransactionAsync(request),
            _ => throw new NotImplementedException()
        };
        
        // TODO: Save the transaction to db if success
    }
}