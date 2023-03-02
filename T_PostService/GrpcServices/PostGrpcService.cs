using Grpc.Core;

namespace T_PostService.GrpcServices;

public class PostGrpcService : PostGrpc.PostGrpcBase
{
    private readonly ILogger<PostGrpcService> _logger;

    public PostGrpcService(ILogger<PostGrpcService> logger)
    {
        _logger = logger;
    }


    public override async Task<PostReply> GetPostById(GetPostByIdRequest request, ServerCallContext context)
    {
        return new PostReply()
        {
            Id =  Guid.NewGuid().ToString()
        };
    }
}