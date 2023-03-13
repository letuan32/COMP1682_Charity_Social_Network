using MediatR;
using TPostService.Services;
using TPostService.ViewModels;

namespace TPostService.Queries;

public class GetPostDetailQueryHandler : IRequestHandler<GetPostDetailQuery, PostViewModel?>
{
    private readonly IPostService _postService;

    public GetPostDetailQueryHandler(IPostService postService)
    {
        _postService = postService;
    }

    public async Task<PostViewModel?> Handle(GetPostDetailQuery request, CancellationToken cancellationToken)
    {
        return await _postService.GetPostByIdAsync(request.PostId);
    }
}