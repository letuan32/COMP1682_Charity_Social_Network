using AutoMapper;
using T_PostService.ViewModels;

namespace T_PostService.MapperProfiles;

public class PostMapperProfile : Profile
{
    public PostMapperProfile()
    {
        CreateMap<GetPostsReplyItem, PostViewModel>().ReverseMap();
        CreateMap<IList<PostViewModel>, GetPostsReply>()
            .ForMember(d => d.Posts, 
                opt => opt.MapFrom(d => d));
    }
}