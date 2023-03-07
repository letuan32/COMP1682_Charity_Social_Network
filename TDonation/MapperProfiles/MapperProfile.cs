using AutoMapper;
using TDonation.ViewModels;

namespace TDonation.MapperProfiles;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<DonationBankingInformationViewModel, GetPostDonationBakingInformationReply>().ReverseMap();
    }
}