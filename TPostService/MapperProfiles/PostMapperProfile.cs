using System.Collections.Generic;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
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
            .ReverseMap();
        
        CreateMap<PostViewModel, PostReply>()
            .ForMember(d => d.Author, opt => opt.MapFrom(s => s.Author))
            .ForMember(d => d.CreatedAt, opt => opt.MapFrom(s => Timestamp.FromDateTime(s.CreatedAt.ToUniversalTime())))
            .ReverseMap();
        CreateMap<IList<PostViewModel>, GetPostsReply>()
            .ForMember(d => d.Posts, 
                opt => opt.MapFrom(d => d));

        CreateMap<GetDonationBankingDescriptionReply, PostBakingDescriptionViewModel>().ReverseMap();

    }
}