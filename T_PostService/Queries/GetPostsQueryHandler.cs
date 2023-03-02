using MediatR;
using T_PostService.Services;
using T_PostService.ViewModels;

namespace T_PostService.Queries;

public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, IList<PostViewModel>?>
{
    private readonly IPostService _postService;
    private readonly ILogger<GetPostsQueryHandler> _logger;
    public GetPostsQueryHandler(IPostService postService, ILogger<GetPostsQueryHandler> logger)
    {
        _postService = postService;
        _logger = logger;
    }

    public async Task<IList<PostViewModel>?> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        return await _postService.GetPostsAsync();
    }
}