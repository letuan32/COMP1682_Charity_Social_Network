using System.Globalization;
using APIGateway.DTOs.Donations;
using APIGateway.Helpers;
using APIGateway.Services;
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
    private readonly IUserService _userService ;

    
    public CreateDonationTransactionCommandHandler(ILogger<CreateDonationTransactionCommandHandler> logger, IMapper mapper, PostGrpc.PostGrpcClient postGrpcClient, Payment.PaymentClient paymentGrpcClient, IUserService userService)
    {
        _logger = logger;
        _mapper = mapper;
        _postGrpcClient = postGrpcClient;
        _paymentGrpcClient = paymentGrpcClient;
        _userService = userService;
    }

    public async Task<CreateDonationTransactionResponse> Handle(CreateDonationTransactionCommand request, CancellationToken cancellationToken)
    {
        var createTransactionGrpcRequest = await MapToGrpcRequest(request);
        
        _logger.LogInformation("Start gRPC request to create transaction. gRPC Servier: {GrpcServer}", _paymentGrpcClient.GetType());
        var createTransactionReply = await _paymentGrpcClient.CreateTransactionAsync(createTransactionGrpcRequest, cancellationToken: cancellationToken);
        return _mapper.Map<CreateDonationTransactionResponse>(createTransactionReply);
    }

    private async Task<CreateTransactionRequest> MapToGrpcRequest(CreateDonationTransactionCommand request)
    {
        // TODO: Pending implement transaction desciption
        // _logger.LogInformation("Start gRPC request to get banking description. gRPC Servier: {GrpcServer}", _postGrpcClient.GetType());
        // var bankingDescription =
        //     await _postGrpcClient.GetPostDonationBankingDescriptionAsync(new GetDonationBankingDescriptionRequest()
        //         { PostId = request.PostId });

        // if (bankingDescription == null) throw new BadHttpRequestException("Post not found");

        return new CreateTransactionRequest()
        {
            PostId = request.PostId,
            Amount = request.Amount,
            BankingType = request.BankingType == null ? 0 : (int)request.BankingType,
            PaymentService = (int)request.PaymentService,
            Description = "Donation",
            UserId = _userService.GetUserIdAsync() ?? "1312321" //TODO: Get real user id
        };
    }
}