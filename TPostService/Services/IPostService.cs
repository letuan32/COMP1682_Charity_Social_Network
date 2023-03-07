using System.Collections.Generic;
using System.Threading.Tasks;
using TPostService.ViewModels;

namespace TPostService.Services;

public interface IPostService
{
    Task<IList<PostViewModel>?> GetPostsAsync();
}