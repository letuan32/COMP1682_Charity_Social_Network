﻿using MediatR;
using TPostService.Services;
using TPostService.ViewModels;

namespace TPostService.CQRS.Queries;

public class GetPostBankingDescriptionQueryHandler : IRequestHandler<GetPostBankingDescriptionQuery, PostBakingDescriptionViewModel?>
{
    private readonly IPostService _postService;
    private readonly ILogger<GetPostBankingDescriptionQueryHandler> _logger;

    public GetPostBankingDescriptionQueryHandler(IPostService postService, ILogger<GetPostBankingDescriptionQueryHandler> logger)
    {
        _postService = postService;
        _logger = logger;
    }

    public async Task<PostBakingDescriptionViewModel?> Handle(GetPostBankingDescriptionQuery request, CancellationToken cancellationToken)
    {
        return await _postService.GetPostBankingDescriptionAsync(request.PostId);
    }
}