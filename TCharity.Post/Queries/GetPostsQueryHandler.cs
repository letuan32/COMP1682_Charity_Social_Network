using MediatR;
using TCharity.Post.ViewModels;

namespace TCharity.Post.Queries;

public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, List<PostViewModel>>
{
    public Task<List<PostViewModel>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}