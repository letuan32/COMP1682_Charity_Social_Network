using AutoMapper;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using TPostService.CQRS.Commands;
using TPostService.CQRS.Queries;
using TPostService.Services;


namespace TPostService.GrpcServices;

public class PostGrpcService : PostGrpc.PostGrpcBase
{
    private readonly ILogger<PostGrpcService> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;


    
    public PostGrpcService(ILogger<PostGrpcService> logger, IMapper mapper, IMediator mediator, IUserService userService)
    {
        _mapper = mapper;
        _mediator = mediator;
        _userService = userService;
        _logger = logger;
    }

    public override async Task<GetPostsReply> GetUnApprovePosts(GetUnApprovePostsRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Receive grpc GetUnApprovePosts request. {Request}", request);
        var response = await _mediator.Send(new GetPostsQuery(){IsApproved = false});

        if (response != null)
        {
            return _mapper.Map<GetPostsReply>(response);
        }
        context.Status = new Status(StatusCode.NotFound, "Not found");

        return null;
    }


    public override async Task<PostReply> GetPostById(GetPostByIdRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Receive request get post detail. Id = {id}", request.Id);
        var response = await _mediator.Send(new GetPostDetailQuery() { PostId = request.Id });
        
        if (response != null)
        {
            return _mapper.Map<PostReply>(response);
        }
        context.Status = new Status(StatusCode.NotFound, "Not found");

        return null;
    }

    public override async Task<GetPostsReply> GetPosts(GetPostsRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Receive grpc GetPosts request. {Request}", request);
        var response = await _mediator.Send(new GetPostsQuery());

        if (response != null)
        {
            return _mapper.Map<GetPostsReply>(response);
        }
        context.Status = new Status(StatusCode.NotFound, "Not found");

        return null;
    }

    [Authorize]
    public override async Task<GetDonationBankingDescriptionReply> GetPostDonationBankingDescription(GetDonationBankingDescriptionRequest request, ServerCallContext context)
    {
        var user = _userService.GetUserId();
        _logger.LogInformation("Receive grpc GetDonationBankingDescriptionRequest request. {Request} ", request);
        var response = await _mediator.Send(new GetPostBankingDescriptionQuery(request.PostId));

        if (response != null)
        {
            return _mapper.Map<GetDonationBankingDescriptionReply>(response);
        }
        context.Status = new Status(StatusCode.NotFound, "Not found");

        return null;
    }

    [Authorize]
    public override async Task<CreatePostResponse> CreatePost(CreatePostRequest request, ServerCallContext context)
    {
        var command = _mapper.Map<CreatePostCommand>(request);
        var response = await _mediator.Send(command);
        
        if (response)
        {
            return new CreatePostResponse() { Success = true };
        }
        context.Status = new Status(StatusCode.InvalidArgument, "Failed to create post");

        return new CreatePostResponse() { Success = false };
    }
}