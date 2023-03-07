using System.Collections.Generic;
using MediatR;
using TPostService.ViewModels;

namespace TPostService.Queries;

public class GetPostsQuery : IRequest<IList<PostViewModel>?>
{
    
}