using MediatR;
using TDonation.ViewModels;

namespace TDonation.CQRS.Queries;

public class GetDonationBankingInformationQuery : IRequest<GetPostDonationBakingInformationReply>
{
    public int PostId { get; set; }
}