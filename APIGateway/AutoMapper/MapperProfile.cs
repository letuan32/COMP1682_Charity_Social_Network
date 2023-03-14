using APIGateway.DTOs.Donations;
using AutoMapper;
using TDonation;

namespace APIGateway.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<CreateTransactionReply, CreateDonationTransactionResponse>()
            .ForMember(d => d.RedirecUrl, opt => opt.MapFrom(s => s.OrderUrl));
    }
}