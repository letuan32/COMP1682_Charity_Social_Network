using System.Globalization;
using APIGateway.DTOs.Donations;
using APIGateway.Helpers;
using AutoMapper;
using MediatR;
using TDonation;
using TPostService;

namespace APIGateway.CQRS.Commands;

public class CreateDonationTransactionCommandHandler : IRequestHandler<CreateDonationTransactionCommand,CreateDonationTransactionResponse>
{
    private readonly Payment.PaymentClient _paymentGrpcClient;
    private readonly PostGrpc.PostGrpcClient _postGrpcClient;

    private readonly ILogger<CreateDonationTransactionCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly UserPropertyHelper _userPropertyHelper;

    
    public CreateDonationTransactionCommandHandler(ILogger<CreateDonationTransactionCommandHandler> logger, IMapper mapper, PostGrpc.PostGrpcClient postGrpcClient, Payment.PaymentClient paymentGrpcClient, UserPropertyHelper userPropertyHelper)
    {
        _logger = logger;
        _mapper = mapper;
        _postGrpcClient = postGrpcClient;
        _paymentGrpcClient = paymentGrpcClient;
        _userPropertyHelper = userPropertyHelper;
    }

    public async Task<CreateDonationTransactionResponse> Handle(CreateDonationTransactionCommand request, CancellationToken cancellationToken)
    {
        var usreId = _userPropertyHelper.GetNameIdentifier() ?? "ADSADAEF";
        
        var createTransactionGrpcRequest = await MapToGrpcRequest(request);
        
        _logger.LogInformation("Start gRPC request to create transaction. gRPC Servier: {GrpcServer}", _paymentGrpcClient.GetType());
        var createTransactionReply = await _paymentGrpcClient.CreateTransactionAsync(createTransactionGrpcRequest, cancellationToken: cancellationToken);
        return _mapper.Map<CreateDonationTransactionResponse>(createTransactionReply);
    }

    private async Task<CreateTransactionRequest> MapToGrpcRequest(CreateDonationTransactionCommand request)
    {
        _logger.LogInformation("Start gRPC request to get banking description. gRPC Servier: {GrpcServer}", _postGrpcClient.GetType());
        var bankingDescription =
            await _postGrpcClient.GetPostDonationBankingDescriptionAsync(new GetDonationBankingDescriptionRequest()
                { PostId = request.PostId });

        if (bankingDescription == null) throw new BadHttpRequestException("Post not found");

        return new CreateTransactionRequest()
        {
            PostId = request.PostId,
            Amount = request.Amount,
            BankingType = (int)request.BankingType,
            PaymentService = (int)request.PaymentService,
            Description = bankingDescription.Description,
            UserId = _userPropertyHelper.GetNameIdentifier() ?? "1312321",
        };
    }
}