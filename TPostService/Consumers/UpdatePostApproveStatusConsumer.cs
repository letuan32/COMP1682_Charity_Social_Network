
using MassTransit;
using SharedModels.Post;
using TPostService.Services;

namespace TPostService.Consumers;

public class UpdatePostApproveStatusConsumer : IConsumer<UpdatePostApproveStatusMessage>
{

    private readonly ILogger<UpdatePostApproveStatusConsumer> _logger;
    private readonly IPostService _postService;

    public UpdatePostApproveStatusConsumer(ILogger<UpdatePostApproveStatusConsumer> logger, IPostService postService)
    {
        _logger = logger;
        _postService = postService;
    }

    public async Task Consume(ConsumeContext<UpdatePostApproveStatusMessage> context)
    {
        var isUpdateSuccess =
            await _postService.UpdateApproveStatusAsync(context.Message.PostId, context.Message.PostApproveStatusEnum);

        if (isUpdateSuccess)
        {
            // TODO: Call Notification serverless function to send notification to user
        }
    }
}