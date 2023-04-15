using MediatR;
using TPostService.Services;

namespace TPostService.CQRS.Commands;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, bool>
{
    private readonly IPostService _postService;
    private readonly IFirebaseService _firebaseService; 
    private readonly ILogger<CreatePostCommandHandler> _logger; 

    public CreatePostCommandHandler(IPostService postService, IFirebaseService firebaseService, ILogger<CreatePostCommandHandler> logger)
    {
        _postService = postService;
        _firebaseService = firebaseService;
        _logger = logger;
    }
    
    public async Task<bool> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        // Create post in database
        var postId = await _postService.CreatePostAsync(request);
        
        if(postId == 0)
        {
            _logger.LogError("Post creation failed");
            return false;
        }
        
        // Create a real-time collection in Firebase
        await _firebaseService.CreatePostRealTimeRecordAsync(postId);

        return true;
    }
}