using MediatR;
using TPostService.ViewModels;

namespace TPostService.CQRS.Queries;

public class GetPostDetailQuery : IRequest<PostViewModel>
{
    public int PostId { get; set; }
}