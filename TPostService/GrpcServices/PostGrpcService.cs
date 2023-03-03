using AutoMapper;
using Grpc.Core;
using T_PostService.Services;

namespace T_PostService.GrpcServices;

public class PostGrpcService : PostGrpc.PostGrpcBase
{
    private readonly ILogger<PostGrpcService> _logger;
    private readonly IPostService _postService;
    private readonly IMapper _mapper;

    
    public PostGrpcService(ILogger<PostGrpcService> logger, IPostService postService, IMapper mapper)
    {
        _postService = postService;
        _mapper = mapper;
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
        var response = await _postService.GetPostsAsync();

        if (response != null)
        {
            return _mapper.Map<GetPostsReply>(response);
        }
        context.Status = new Status(StatusCode.NotFound, "Not found");

        return null;
    }
}