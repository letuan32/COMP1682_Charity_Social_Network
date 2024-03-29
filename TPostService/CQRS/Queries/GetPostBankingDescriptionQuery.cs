﻿using MediatR;
using TPostService.ViewModels;

namespace TPostService.CQRS.Queries;

public class GetPostBankingDescriptionQuery : IRequest<PostBakingDescriptionViewModel?>
{
    public int PostId { get; set; }

    public GetPostBankingDescriptionQuery(int postId)
    {
        PostId = postId;
    }
}