using AutoMapper;
using Newtonsoft.Json;
using SharedModels.Enums;
using SharedModels.Paypal;
using TDonation.CQRS.Commands;
using TDonation.CQRS.ViewModels;
using TDonation.Entities;
using TDonation.Enums;
using TDonation.Services.DTOs.Paypal;
using TDonation.Services.DTOs.ZaloPay;
using TDonation.Services.Interfaces;

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
            .ConstructUsing(_ => new CreateTransactionResponse(_.OrderUrl, _.OrderToken, _.ReturnMessage));
        CreateMap<HandleZaloCallbackResponse, HandleZaloCallbackReply>()
            .ForMember(d => d.ReturnMessage, opt => opt.MapFrom(s => s.ReturnMessage))
            .ForMember(d => d.ReturnCode, opt => opt.MapFrom(s => s.ReturnCode));

        CreateMap<HandleZaloCallbackRequest, HandleZaloCallbackCommand>()
            .ForMember(d => d.Mac, opt => opt.MapFrom(s => s.Mac))
            .ForMember(d => d.Data, opt => opt.MapFrom(s => s.Data))
            .ForMember(d => d.Type, opt => opt.MapFrom(s => s.Type));

        // Create mappings for HandleZaloCallbackCommand and HandleZaloCallbackResponse



        CreateMap<PaypalCaptureResponse, DonationTransactionEntity>(MemberList.None)
            .ForMember(dest => dest.InternalTransactionId, opt => opt.MapFrom(src => src.InvoiceId))
            .ForMember(dest => dest.StatusEnum,
                opt => opt.MapFrom(src =>
                    src.Status == "COMPLETED" ? TransactionStatusEnum.Success : TransactionStatusEnum.InProcess))
            .ForMember(d => d.Amount, opt => opt.MapFrom(s => (long)float.Parse(s.Amount.Value)))
            .ForMember(d => d.CurrencyEnum, opt => 
                opt.MapFrom(s => "USD"))
            .ForMember(d => d.InternalSenderId, opt => opt.MapFrom(s => s.CustomId))
            .ForMember(d => d.TransactionTypeEnum, opt => opt.MapFrom(s => TransactionTypeEnum.Donation))
            .ForMember(d => d.PaymentServiceEnum, opt => opt.MapFrom(s => PaymentServiceEnum.Paypal))
            .ForMember(d => d.TransactionToken, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.Description, opt => opt.MapFrom(s => $"Donation"))
            .ForMember(d => d.InternalReceiverId, opt => opt.MapFrom(s => ""))
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ForMember(d => d.CreatedBy, opt => opt.MapFrom(s => s.CustomId))
            .ForMember(d => d.UpdatedBy, opt => opt.MapFrom(s => s.CustomId))
            .ForMember(dest => dest.AdditionalData, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src)));
        

    }
}