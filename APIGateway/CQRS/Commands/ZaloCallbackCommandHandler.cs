using System.Globalization;
using APIGateway.DTOs.Donations;
using APIGateway.Helpers;
using AutoMapper;
using MediatR;
using TDonation;
using TPostService;

namespace APIGateway.CQRS.Commands;

public class ZaloCallbackCommandHandler : IRequestHandler<ZaloCallbackCommand,Unit>
{
    private readonly Payment.PaymentClient _paymentGrpcClient;
    private readonly PostGrpc.PostGrpcClient _postGrpcClient;

    private readonly ILogger<CreateDonationTransactionCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly UserPropertyHelper _userPropertyHelper;


    public Task<Unit> Handle(ZaloCallbackCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}