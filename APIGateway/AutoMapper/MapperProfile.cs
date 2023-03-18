using APIGateway.CQRS.Commands;
using APIGateway.DTOs.Donations;
using AutoMapper;
using SharedModels;
using TDonation;

namespace APIGateway.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<CreateTransactionReply, CreateDonationTransactionResponse>()
            .ForMember(d => d.RedirecUrl, opt => opt.MapFrom(s => s.OrderUrl));

        CreateMap<HandleZaloCallbackRequest, ZaloCallbackCommand>()
            .ForMember(d => d.Data, opt => opt.MapFrom(s => s.Data))
            .ForMember(d => d.Type, opt => opt.MapFrom(s => s.Type))
            .ForMember(d => d.Mac, opt => opt.MapFrom(s => s.Mac))
            .ReverseMap();

        CreateMap<HandleZaloCallbackReply, HandleZaloCallbackResponse>()
            .ForMember(d => d.ReturnCode, opt => opt.MapFrom(s => s.ReturnCode))
            .ForMember(d => d.ReturnMessage, opt => opt.MapFrom(s => s.ReturnMessage));
    }
}