using System.Globalization;
using APIGateway.DTOs.Donations;
using APIGateway.Helpers;
using AutoMapper;
using MassTransit;
using MediatR;
using SharedModels;
using TDonation;
using TPostService;

namespace APIGateway.CQRS.Commands;

public class ZaloCallbackCommandHandler : IRequestHandler<ZaloCallbackCommand,HandleZaloCallbackResponse>
{
    private readonly Payment.PaymentClient _paymentGrpcClient;
    private readonly PostGrpc.PostGrpcClient _postGrpcClient;

    private readonly ILogger<ZaloCallbackCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly UserPropertyHelper _userPropertyHelper;
    private readonly IBus _bus;


    public ZaloCallbackCommandHandler(Payment.PaymentClient paymentGrpcClient, PostGrpc.PostGrpcClient postGrpcClient, ILogger<ZaloCallbackCommandHandler> logger, IMapper mapper, UserPropertyHelper userPropertyHelper, IBus bus)
    {
        _paymentGrpcClient = paymentGrpcClient;
        _postGrpcClient = postGrpcClient;
        _logger = logger;
        _mapper = mapper;
        _userPropertyHelper = userPropertyHelper;
        _bus = bus;
    }

    public async Task<HandleZaloCallbackResponse> Handle(ZaloCallbackCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start gRPC request to handle zalo callback. gRPC Servier: {GrpcServer}", _postGrpcClient.GetType());
        var response =  await _paymentGrpcClient.HandleZaloCallbackAsync(_mapper.Map<HandleZaloCallbackRequest>(request));
        return _mapper.Map<HandleZaloCallbackResponse>(response);
    }
}