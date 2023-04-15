using System.Collections.Generic;
using System.Threading.Tasks;
using SharedModels.Enums;
using TPostService.CQRS.Commands;
using TPostService.ViewModels;

namespace TPostService.Services;

public interface IPostService
{
    Task<IList<PostViewModel>?> GetApprovedPostsAsync();
    Task<IList<PostViewModel>?> GetPrivatePostsAsync();
    Task<PostViewModel?> GetApprovedPostByIdAsync(int postId);
    Task<int> CreatePostAsync(CreatePostCommand postViewModel);
    Task<PostBakingDescriptionViewModel?> GetPostBankingDescriptionAsync(int postId);
    Task<bool> UpdateApproveStatusAsync(int postId, PostApproveStatusEnum postApproveStatus);

}