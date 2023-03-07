using TDonation.Services.Interfaces;
using TDonation.ViewModels;

namespace TDonation.Services;

public class PostService : IPostService
{
    public Task<DonationBankingInformationViewModel> GetPostDonationBakingInformationAsync(int postId)
    {
        // TODO: Implement database query
        var vm = new DonationBankingInformationViewModel()
        {
            PostId = 1,
            BankingDescription = "TD01"
        };

        return Task.FromResult<DonationBankingInformationViewModel>(vm);
    }
}