using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SharedModels.Enums;
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

    public async Task<IList<PostViewModel>?> GetApprovedPostsAsync()
    {
        var postViewModels = await _postDbContext.PostEntities
            .Where(p => p.ApproveStatusEnum == PostApproveStatusEnum.Approved || p.ApproveStatusEnum == PostApproveStatusEnum.Disbursed)
            .OrderByDescending(p => p.Id)
            .Include(p => p.CommentsEntities)
            .ProjectTo<PostViewModel>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync();

        if (!postViewModels.Any())
        {
            return null;
        }

        foreach (var post in postViewModels)
        {
            var user = await _firebaseService.GetUserAsync(post.CreatedById);
            post.Author = _mapper.Map<PostAuthorViewModel>(user);
        }

        return postViewModels;
    }

    public async Task<IList<PostViewModel>?> GetPrivatePostsAsync()
    {
        var postViewModels = await _postDbContext.PostEntities
            .Where(p => p.ApproveStatusEnum != PostApproveStatusEnum.Approved && p.ApproveStatusEnum != PostApproveStatusEnum.Disbursed)
            .Include(p => p.CommentsEntities)
            .ProjectTo<PostViewModel>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync();

        if (!postViewModels.Any())
        {
            return null;
        }

        foreach (var post in postViewModels)
        {
            var user = await _firebaseService.GetUserAsync(post.CreatedById);
            post.Author = _mapper.Map<PostAuthorViewModel>(user);
        }

        return postViewModels;
    }

    public async Task<PostViewModel?> GetApprovedPostByIdAsync(int postId)
    {
        var postViewModel = await _postDbContext.PostEntities
            // .Where(p => p.ApproveStatusEnum == PostApproveStatusEnum.Approved)
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

    public async Task<int> CreatePostAsync(CreatePostCommand postViewModel)
    {
        var entity = _mapper.Map<PostEntity>(postViewModel);
        await _postDbContext.PostEntities.AddAsync(entity);
        var result = await _postDbContext.SaveChangesAsync();
        return entity.Id;
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

    public async Task<bool> UpdateApproveStatusAsync(int postId, PostApproveStatusEnum postApproveStatus)
    {
        var entity = await _postDbContext.PostEntities.FirstOrDefaultAsync(p => p.Id == postId);

        if (entity == null) return false;

        entity.ApproveStatusEnum = postApproveStatus;
        var result = await _postDbContext.SaveChangesAsync();
        return result > 0;
    }
}