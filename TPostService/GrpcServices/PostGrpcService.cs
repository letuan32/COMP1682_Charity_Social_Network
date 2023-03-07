using System;
using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using T_PostService;
using TPostService.Queries;
using TPostService.Services;


namespace TPostService.GrpcServices;

public class PostGrpcService : PostGrpc.PostGrpcBase
{
    private readonly ILogger<PostGrpcService> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    
    public PostGrpcService(ILogger<PostGrpcService> logger, IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
        _logger = logger;
    }


    public override async Task<PostReply> GetPostById(GetPostByIdRequest request, ServerCallContext context)
    {
        // TODO: Implement
        return new PostReply()
        {
            Id =  new Random().Next(1,10)
        };
    }

    public override async Task<GetPostsReply> GetPosts(GetPostsRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Receive request");
        var response = await _mediator.Send(new GetPostsQuery());

        if (response != null)
        {
            return _mapper.Map<GetPostsReply>(response);
        }
        context.Status = new Status(StatusCode.NotFound, "Not found");

        return null;
    }

    public override async Task<GetDonationBankingDescriptionReply> GetPostDonationBankingDescription(GetDonationBankingDescriptionRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Receive grpc request: GetDonationBankingDescriptionRequest ");
        var response = await _mediator.Send(new GetPostBankingDescriptionQuery(request.PostId));

        if (response != null)
        {
            return _mapper.Map<GetDonationBankingDescriptionReply>(response);
        }
        context.Status = new Status(StatusCode.NotFound, "Not found");

        return null;
    }
}