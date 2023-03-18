using AutoMapper;
using Grpc.Core;
using MediatR;
using TDonation.CQRS.Commands;

namespace TDonation.GrpcServices;

public class PaymentGrpcService : Payment.PaymentBase
{
    private readonly ILogger<PaymentGrpcService> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;


    public PaymentGrpcService(ILogger<PaymentGrpcService> logger, IMediator mediator, IMapper mapper)
    {
        _logger = logger;
        _mediator = mediator;
        _mapper = mapper;
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