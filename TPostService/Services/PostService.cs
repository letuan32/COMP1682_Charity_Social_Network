using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TPostService.CQRS.Commands;
using TPostService.Entities;
using TPostService.Infrastructure;
using TPostService.ViewModels;

namespace TPostService.Services;

public class PostService : IPostService
{
    private readonly IFirebaseService _firebaseService;
    private readonly PostDbContext _postDbContext;
    private readonly IMapper _mapper;

    public PostService(IFirebaseService firebaseService, PostDbContext postDbContext, IMapper mapper)
    {
        _firebaseService = firebaseService;
        _postDbContext = postDbContext;
        _mapper = mapper;
    }

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
                MediaUrls = new List<string> { "https://example.com/image1.jpg", "https://example.com/image2.jpg" },
                NumberOfComment = 2,
                Author = new PostAuthorViewModel()
                {
                    Email = "test@gmail.com",
                    DisplayName = "Test Name",
                    AvatarUrl = "https://gravatar.com/avatar/3fa2989800b80fc30df5ebcb707d7637?s=400&d=robohash&r=x"
                },
                Location = "Da Nang",
                CreatedAt = DateTime.Now

            },
            new()
            {
                Id = 2,
                Content = "This is the second post",
                NumberOfDonation = 10,
                CreatedById = "user2",
                MediaUrls = new List<string> { "https://example.com/image2-1.jpg", "https://example.com/image2-2.jpg" },
                NumberOfComment = 3,
                Author = new()
                {
                    Email = "letuanlttt@gmail.com",
                    DisplayName = "Tuan Le",
                    AvatarUrl = "https://gravatar.com/avatar/96f9e4848bbcd456d5640e142dc04072?s=400&d=robohash&r=x"
                },
                Location = "Da Nang",
                CreatedAt = DateTime.Now
            }
        };

        return await Task.FromResult<IList<PostViewModel>>(viewModels);
    }

    public async Task<PostViewModel?> GetPostByIdAsync(int postId)
    {
        var postViewModel = await _postDbContext.PostEntities
            .Include(p => p.CommentsEntities)
            .ProjectTo<PostViewModel>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == postId);
        
        if (postViewModel == null)
        {
            return null;
        }
        
        var user = await _firebaseService.GetUserAsync(postViewModel.CreatedById);
        postViewModel.Author = _mapper.Map<PostAuthorViewModel>(user);

        return postViewModel;
    }

    public async Task<bool> CreatePostAsync(CreatePostCommand postViewModel)
    {
        var entity = _mapper.Map<PostEntity>(postViewModel);
        await _postDbContext.PostEntities.AddAsync(entity);
        var result = await _postDbContext.SaveChangesAsync();
        return result > 0;
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