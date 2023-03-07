using TDonation.ViewModels;

namespace TDonation.Services.Interfaces;

public interface IPostService
{
    Task<DonationBankingInformationViewModel> GetPostDonationBakingInformationAsync(int postId);

}