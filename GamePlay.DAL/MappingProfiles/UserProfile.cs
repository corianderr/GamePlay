using AutoMapper;
using GamePlay.Domain.Entities;
using GamePlay.Domain.Models.User;

namespace GamePlay.DAL.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserModel, ApplicationUser>();
    }
}