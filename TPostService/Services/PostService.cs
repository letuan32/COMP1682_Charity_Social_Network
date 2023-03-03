using T_PostService.ViewModels;

namespace T_PostService.Services;

public class PostService : IPostService
{
    public async Task<IList<PostViewModel>?> GetPostsAsync()
    {
        // TODO: Implement
        return new List<PostViewModel>()
        {
            new()
            {
                Id = 1,
                Content = "This is a sample post",
                NumberOfDonation = 10,
                CreatedById = "user123",
                ImageUrls = new List<string> { "https://example.com/image1.jpg", "https://example.com/image2.jpg" },
                VideoUrls = new List<string> { "https://example.com/video1.mp4" },
                NumberOfComment = 2
            },
            new()
            {
                Id = 2,
                Content = "This is the second post",
                NumberOfDonation = 10,
                CreatedById = "user2",
                ImageUrls = new List<string> { "https://example.com/image2-1.jpg", "https://example.com/image2-2.jpg" },
                VideoUrls = null,
                NumberOfComment = 3
            }
        };
    }
}