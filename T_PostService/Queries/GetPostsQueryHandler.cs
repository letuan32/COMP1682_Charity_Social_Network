using MediatR;
using T_PostService.ViewModels;

namespace T_PostService.Queries;

public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, List<PostViewModel>>
{
    public Task<List<PostViewModel>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}