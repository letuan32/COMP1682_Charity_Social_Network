using APIGateway.DTOs.Donations;
using AutoMapper;
using MediatR;
using TDonation;
using TPostService;

namespace APIGateway.CQRS.Commands;

public class DisburseCommandHandler : IRequestHandler<DisburseCommand, DisburseDonationReply>
{
    private readonly Payment.PaymentClient _paymentGrpcClient;
    private readonly PostGrpc.PostGrpcClient _postGrpcClient;

    private readonly ILogger<DisburseCommandHandler> _logger;
    private readonly IMapper _mapper;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="paymentGrpcClient"></param>
    /// <param name="postGrpcClient"></param>
    /// <param name="logger"></param>
    /// <param name="mapper"></param>
    public DisburseCommandHandler(Payment.PaymentClient paymentGrpcClient, PostGrpc.PostGrpcClient postGrpcClient, ILogger<DisburseCommandHandler> logger, IMapper mapper)
    {
        _paymentGrpcClient = paymentGrpcClient;
        _postGrpcClient = postGrpcClient;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<DisburseDonationReply> Handle(DisburseCommand request, CancellationToken cancellationToken)
    {
        var response = await _paymentGrpcClient.DisburseDonationAsync(new DisburseDonationRequest()
        {
            PostId = request.PostId,
            UserEmail = request.UserEmail
        }, cancellationToken: cancellationToken);
        
        if(response == null) throw new BadHttpRequestException("Donation not found");

        return response;
    }
}