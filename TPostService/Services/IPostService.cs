using System.Collections.Generic;
using System.Threading.Tasks;
using TPostService.CQRS.Commands;
using TPostService.ViewModels;

namespace TPostService.Services;

public interface IPostService
{
    Task<IList<PostViewModel>?> GetPostsAsync();
    Task<PostViewModel?> GetPostByIdAsync(int postId);
    Task<bool> CreatePostAsync(CreatePostCommand postViewModel);
    
    Task<PostBakingDescriptionViewModel?> GetPostBankingDescriptionAsync(int postId);

}