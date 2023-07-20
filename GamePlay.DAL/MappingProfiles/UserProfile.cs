using AutoMapper;
using GamePlay.Domain.Entities;
using GamePlay.Domain.Models.User;

namespace GamePlay.DAL.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserModel, ApplicationUser>()
            .ForMember(g => g.FollowersCount, map => map.MapFrom(g => 0))
            .ForMember(g => g.FriendsCount, map => map.MapFrom(g => 0));
        CreateMap<UserModel, ApplicationUser>();
        CreateMap<ApplicationUser, UserModel>();
        CreateMap<CreateUserRelationModel, UserRelation>();
        CreateMap<UserRelation, UserRelationModel>();
    }
}