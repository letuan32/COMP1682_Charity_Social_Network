using MediatR;
using TPostService.ViewModels;

namespace TPostService.CQRS.Queries;

public class GetPostsQuery : IRequest<IList<PostViewModel>?>
{
    
}