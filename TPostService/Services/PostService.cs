using System.Collections.Generic;
using System.Threading.Tasks;
using TPostService.ViewModels;

namespace TPostService.Services;

public class PostService : IPostService
{
    public async Task<IList<PostViewModel>?> GetPostsAsync()
    {
        // TODO: Implement
        var viewModels = new List<PostViewModel>()
        {
            new()
            {
                Id = 1,
                Content = "This is a sample post",
                NumberOfDonation = 10,
                CreatedById = "user123",
                ImageUrls = new List<string> { "https://example.com/image1.jpg", "https://example.com/image2.jpg" },
                VideoUrls = new List<string> { "https://example.com/video1.mp4" },
                NumberOfComment = 2,
                Author = new PostAuthorViewModel()
                {
                    Email = "test@gmail.com",
                    DisplayName = "Test Name",
                    AvatarUrl = "https://gravatar.com/avatar/3fa2989800b80fc30df5ebcb707d7637?s=400&d=robohash&r=x"
                },
                Location = "Da Nang",
                Likes = 132,
                CreatedAt = DateTime.Now

            },
            new()
            {
                Id = 2,
                Content = "This is the second post",
                NumberOfDonation = 10,
                CreatedById = "user2",
                ImageUrls = new List<string> { "https://example.com/image2-1.jpg", "https://example.com/image2-2.jpg" },
                VideoUrls = null,
                NumberOfComment = 3,
                Author = new()
                {
                    Email = "letuanlttt@gmail.com",
                    DisplayName = "Tuan Le",
                    AvatarUrl = "https://gravatar.com/avatar/96f9e4848bbcd456d5640e142dc04072?s=400&d=robohash&r=x"
                },
                Location = "Da Nang",
                Likes = 132,
                CreatedAt = DateTime.Now
            }
        };

        return await Task.FromResult<IList<PostViewModel>>(viewModels);
    }

    public async Task<PostViewModel?> GetPostByIdAsync(int postId)
    {
        var post = new PostViewModel()
        {
            Id = postId,
            Content = "This is a sample post",
            NumberOfDonation = 10,
            CreatedById = "user123",
            ImageUrls = new List<string> { "https://example.com/image1.jpg", "https://example.com/image2.jpg" },
            VideoUrls = new List<string> { "https://example.com/video1.mp4" },
            NumberOfComment = 2,
            Author = new PostAuthorViewModel()
            {
                Email = "test@gmail.com",
                DisplayName = "Test Name",
                AvatarUrl = "https://gravatar.com/avatar/3fa2989800b80fc30df5ebcb707d7637?s=400&d=robohash&r=x"
            },
            Location = "Da Nang",
            Likes = 132,
            CreatedAt = DateTime.Now
        };
        return await Task.FromResult(post);
    }

    public async Task<PostBakingDescriptionViewModel?> GetPostBankingDescriptionAsync(int postId)
    {
        // TODO: Implement
        var viewModel = new PostBakingDescriptionViewModel()
        {
            PostId = 1,
            Description = "T01"
        };

        return await Task.FromResult(viewModel);
    }
}