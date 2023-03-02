using MediatR;
using T_PostService.ViewModels;

namespace T_PostService.Queries;

public class GetPostsQuery : IRequest<IList<PostViewModel>?>
{
    
}