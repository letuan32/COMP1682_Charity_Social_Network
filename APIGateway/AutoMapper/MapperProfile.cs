using APIGateway.CQRS.Commands;
using APIGateway.CQRS.Commands.PostCommands;
using APIGateway.DTOs.Donations;
using APIGateway.DTOs.Users;
using AutoMapper;
using FirebaseAdmin.Auth;
using Google.Protobuf.WellKnownTypes;
using SharedModels;
using TDonation;
using TPostService;

namespace APIGateway.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<CreateTransactionReply, CreateDonationTransactionResponse>()
            .ForMember(d => d.RedirecUrl, opt => opt.MapFrom(s => s.OrderUrl))
            .ForMember(d => d.InternalTransactionId, opt => opt.MapFrom(s => s.TransToken));


        CreateMap<HandleZaloCallbackRequest, ZaloCallbackCommand>()
            .ForMember(d => d.Data, opt => opt.MapFrom(s => s.Data))
            .ForMember(d => d.Type, opt => opt.MapFrom(s => s.Type))
            .ForMember(d => d.Mac, opt => opt.MapFrom(s => s.Mac))
            .ReverseMap();

        CreateMap<HandleZaloCallbackReply, HandleZaloCallbackResponse>()
            .ForMember(d => d.ReturnCode, opt => opt.MapFrom(s => s.ReturnCode))
            .ForMember(d => d.ReturnMessage, opt => opt.MapFrom(s => s.ReturnMessage));

        CreateMap<CreatePostCommand, CreatePostRequest>()
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.ExpectedAmount, opt => opt.MapFrom(src => src.ExpectedAmount))
            .ForMember(dest => dest.ExpectedReceivedDate,
                opt => opt.MapFrom(src => Timestamp.FromDateTime(src.ExpectedReceivedDate.ToUniversalTime())))
            .ForMember(dest => dest.PostCategoryEnum, opt => opt.MapFrom(src => (int)src.PostCategoryEnum))
            .ForMember(dest => dest.CurrencyEnum, opt => opt.MapFrom(src => (int)src.CurrencyEnum))
            .ForMember(dest => dest.MediaUrls, opt => opt.MapFrom(src => src.MediaUrls ?? new List<string>()))
            .ForMember(dest => dest.DocumentUrls, opt => opt.MapFrom(src => src.DocumentUrls ?? new List<string>()));

        CreateMap<UserRecord, GetUserResponse>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Uid))
            .ForMember(d => d.IsDisable, opt => opt.MapFrom(s => s.Disabled))
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
            .ForMember(d => d.DisplayName, opt => opt.MapFrom(s => s.DisplayName))
            .ForMember(d => d.ProfilePicture, opt => opt.MapFrom(s => s.PhotoUrl));
    }
}