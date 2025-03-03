using SocialMediaAPI.DTOs;
using SocialMediaAPI.Models;
using AutoMapper;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<Post, PostDto>().ReverseMap();
        CreateMap<Comment, CommentDto>().ReverseMap();
        CreateMap<Like, LikeDto>().ReverseMap();
        CreateMap<Follow, FollowDto>().ReverseMap();
    }
}