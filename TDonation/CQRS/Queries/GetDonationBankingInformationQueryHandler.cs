using AutoMapper;
using MediatR;
using TDonation.Services.Interfaces;
using TDonation.ViewModels;

namespace TDonation.CQRS.Queries;

public class GetDonationBankingInformationQueryHandler : IRequestHandler<GetDonationBankingInformationQuery, GetPostDonationBakingInformationReply>
{
    private readonly IPostService _postService;
    private readonly IMapper _mapper;
    

    public GetDonationBankingInformationQueryHandler(IPostService postService, IMapper mapper)
    {
        _postService = postService;
        _mapper = mapper;
    }

    public async Task<GetPostDonationBakingInformationReply> Handle(GetDonationBankingInformationQuery request, CancellationToken cancellationToken)
    {
        var vm = await _postService.GetPostDonationBakingInformationAsync(request.PostId);
        return await Task.FromResult(_mapper.Map<GetPostDonationBakingInformationReply>(vm));
    }
}