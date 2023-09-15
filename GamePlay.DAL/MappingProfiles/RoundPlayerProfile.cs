using AutoMapper;
using GamePlay.Domain.Entities;
using GamePlay.Domain.Models.RoundPlayer;

namespace GamePlay.DAL.MappingProfiles;

public class RoundPlayerProfile : Profile {
    public RoundPlayerProfile() {
        CreateMap<CreateRoundPlayerModel, RoundPlayer>();
        CreateMap<RoundPlayer, RoundPlayerModel>();
        CreateMap<RoundPlayerModel, RoundPlayer>();
    }
}