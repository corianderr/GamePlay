using AutoMapper;
using GamePlay.Domain.Entities;
using GamePlay.Domain.Models.Player;

namespace GamePlay.DAL.MappingProfiles;

public class PlayerProfile : Profile {
    public PlayerProfile() {
        CreateMap<CreatePlayerModel, Player>();
        CreateMap<Player, PlayerModel>();
        CreateMap<PlayerModel, Player>();
    }
}