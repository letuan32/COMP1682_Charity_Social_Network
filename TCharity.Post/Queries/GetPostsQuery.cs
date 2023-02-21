using MediatR;
using TCharity.Post.ViewModels;

namespace TCharity.Post.Queries;

public class GetPostsQuery : IRequest<List<PostViewModel>>
{
    
}