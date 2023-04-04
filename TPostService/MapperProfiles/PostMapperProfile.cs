using AutoMapper;
using FirebaseAdmin.Auth;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using SharedModels.Enums;
using TPostService.CQRS.Commands;
using TPostService.Entities;
using TPostService.ViewModels;


namespace TPostService.MapperProfiles;

public class PostMapperProfile : Profile
{
    public PostMapperProfile()
    {
        CreateMap<PostAuthorReply, PostAuthorViewModel>().ReverseMap();
        CreateMap<PostViewModel, PostItemReply>()
            .ForMember(d => d.Author, opt => opt.MapFrom(s => s.Author))
            .ForMember(d => d.CreatedAt, opt => opt.MapFrom(s => Timestamp.FromDateTime(s.CreatedAt.ToUniversalTime())))
            .ForMember(d => d.ExpectedReceivedDate, opt => opt.MapFrom(s => Timestamp.FromDateTime(s.ExpectedReceivedDate.ToUniversalTime())))
            .ForMember(d => d.VideoUrls, opt => opt.MapFrom(s => s.MediaUrls.ToList()))
            .ForMember(d => d.MediaUrls, opt => opt.MapFrom(s => s.MediaUrls.ToList()
            ))
            .ForMember(d => d.DocumentUrls, opt => opt.MapFrom(s => s.DocumentUrls.ToList()))
            .ReverseMap();
        
        CreateMap<PostViewModel, PostReply>()
            .ForMember(d => d.Author, opt => opt.MapFrom(s => s.Author))
            .ForMember(d => d.CreatedAt, opt => opt.MapFrom(s => Timestamp.FromDateTime(s.CreatedAt.ToUniversalTime())))
            .ForMember(d => d.ExpectedReceivedDate, opt => opt.MapFrom(s => Timestamp.FromDateTime(s.ExpectedReceivedDate.ToUniversalTime())))
            .ReverseMap();
        CreateMap<IList<PostViewModel>, GetPostsReply>()
            .ForMember(d => d.Posts, 
                opt => opt.MapFrom(d => d));

        CreateMap<GetDonationBankingDescriptionReply, PostBakingDescriptionViewModel>().ReverseMap();

        
        // Map Post entity to PostViewModel
        CreateMap<UserRecord, PostAuthorViewModel>(MemberList.None)
            .ConstructUsing(s => new PostAuthorViewModel(s.Email, s.DisplayName, s.PhotoUrl));
        CreateMap<PostEntity, PostViewModel>(MemberList.None)
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.Content, opt => opt.MapFrom(s => s.Content))
            .ForMember(d => d.CreatedAt, opt => opt.MapFrom(s => s.CreatedDate))
            .ForMember(d => d.CreatedById, opt => opt.MapFrom(s => s.CreatedBy))
            .ForMember(d => d.MediaUrls, opt => opt.MapFrom(s => s.MediaUrls))
            .ForMember(d => d.DocumentUrls, opt => opt.MapFrom(s => s.DocumentUrls))
            .ForMember(d => d.Location, opt => opt.MapFrom(s => s.Location))
            .ForMember(d => d.ExpectedAmount, opt => opt.MapFrom(s => s.ExpectedAmount))
            .ForMember(d => d.ExpectedReceivedDate, opt => opt.MapFrom(s => s.ExpectedReceivedDate))
            .ForMember(d => d.Views, opt => opt.MapFrom(s => s.Views))
            .ForMember(d => d.Currency, opt => opt.MapFrom(s => s.CurrencyEnum.GetDescription()))
            .ForMember(d => d.Category, opt => opt.MapFrom(s => s.PostCategoryEnum.GetDescription()))
            .ForMember(d => d.ApproveStatus, opt => opt.MapFrom(s => s.ApproveStatusEnum.GetDescription()))
            .ForMember(d => d.NumberOfDonation, opt => opt.MapFrom(s => s.Donations))
            .ForMember(d => d.NumberOfComment,
                opt => opt.MapFrom(s => s.CommentsEntities.Any() ? s.CommentsEntities.Count : 0));
        


        // Map CreatePostCommand to PostEntity
        CreateMap<CreatePostRequest, CreatePostCommand>()
            .ForMember(d => d.Content, opt => opt.MapFrom(s => s.Content))
            .ForMember(d => d.MediaUrls, opt => opt.MapFrom(s => s.MediaUrls))
            .ForMember(d => d.DocumentUrls, opt => opt.MapFrom(s => s.DocumentUrls))
            .ForMember(d => d.Location, opt => opt.MapFrom(s => s.Location))
            .ForMember(d => d.ExpectedAmount, opt => opt.MapFrom(s => s.ExpectedAmount))
            .ForMember(d => d.ExpectedReceivedDate, opt => opt.MapFrom(s => s.ExpectedReceivedDate.ToDateTime()))
            .ForMember(d => d.PostCategoryEnum, opt => opt.MapFrom(s => s.PostCategoryEnum))
            .ForMember(d => d.CurrencyEnum, opt => opt.MapFrom(s => s.CurrencyEnum));

        CreateMap<CreatePostCommand, PostEntity>(MemberList.None)
            .ForMember(d => d.Content, opt => opt.MapFrom(s => s.Content))
            .ForMember(d => d.MediaUrls, opt => opt.MapFrom(s => s.MediaUrls))
            .ForMember(d => d.DocumentUrls, opt => opt.MapFrom(s => s.DocumentUrls))
            .ForMember(d => d.Location, opt => opt.MapFrom(s => s.Location))
            .ForMember(d => d.ExpectedAmount, opt => opt.MapFrom(s => s.ExpectedAmount))
            .ForMember(d => d.ExpectedReceivedDate, opt => opt.MapFrom(s => s.ExpectedReceivedDate))
            .ForMember(d => d.PostCategoryEnum, opt => opt.MapFrom(s => s.PostCategoryEnum))
            .ForMember(d => d.ApproveStatusEnum, opt => opt.MapFrom(s => PostApproveStatusEnum.InProcess))
            .ForMember(d => d.CurrencyEnum, opt => opt.MapFrom(s => s.CurrencyEnum));

    }
}