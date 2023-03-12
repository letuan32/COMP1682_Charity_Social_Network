using AutoMapper;
using TDonation.CQRS.Commands;
using TDonation.CQRS.ViewModels;
using TDonation.Services.DTOs.ZaloPay;

namespace TDonation.MapperProfiles;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<CreateTransactionRequest, CreateTransactionCommand>()
            .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.UserId))
            .ForMember(d => d.PaymentServiceEnum, opt => opt.MapFrom(s => s.PaymentService))
            .ForMember(d => d.UserEmail, opt => opt.MapFrom(s => s.UserEmail))
            .ForMember(d => d.Amount, opt => opt.MapFrom(s => s.Amount))
            .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
            .ForMember(d => d.BankingType, opt => opt.MapFrom(s => s.BankingType))
            .ForMember(d => d.CallbackUrl, opt => opt.MapFrom(s => s.CallbackUrl))
            .ReverseMap();

        CreateMap<CreateTransactionResponse, CreateTransactionReply>()
            .ForMember(d => d.OrderUrl, opt => opt.MapFrom(s => s.PaymentGatewayUrl))
            .ForMember(d => d.Message, opt => opt.MapFrom(s => s.Message))
            .ForMember(d => d.TransToken, opt => opt.MapFrom(s => s.TransactionToken));


        CreateMap<CreateZaloTransactionResponse, CreateTransactionResponse>()
            .ForMember(d => d.PaymentGatewayUrl, opt => opt.MapFrom(s => s.OrderUrl))
            .ForMember(d => d.TransactionToken, opt => opt.MapFrom(s => s.OrderToken))
            .ForMember(d => d.Message, opt => opt.MapFrom(s => s.ReturnMessage));

    }
}