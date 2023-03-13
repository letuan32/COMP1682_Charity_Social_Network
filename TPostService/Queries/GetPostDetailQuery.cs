using MediatR;
using TPostService.ViewModels;

namespace TPostService.Queries;

public class GetPostDetailQuery : IRequest<PostViewModel>
{
    public int PostId { get; set; }
}