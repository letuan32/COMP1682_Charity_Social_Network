using T_PostService.ViewModels;

namespace T_PostService.Services;

public interface IPostService
{
    Task<IList<PostViewModel>?> GetPostsAsync();
}