using MediatR;
using TPostService.Services;

namespace TPostService.CQRS.Commands;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, bool>
{
    private readonly IPostService _postService;
    public CreatePostCommandHandler(IPostService postService)
    {
       _postService = postService;
    }
    
    public async Task<bool> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        return await _postService.CreatePostAsync(request);
    }
}