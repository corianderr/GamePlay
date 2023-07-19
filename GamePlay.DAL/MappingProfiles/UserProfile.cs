using AutoMapper;
using GamePlay.Domain.Entities;
using GamePlay.Domain.Models.User;

namespace GamePlay.DAL.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserModel, ApplicationUser>();
        CreateMap<UserResponseModel, ApplicationUser>();
        CreateMap<ApplicationUser, UserResponseModel>();
        CreateMap<CreateUserRelationModel, UserRelation>();
        CreateMap<UserRelation, UserRelationResponseModel>();
    }
}