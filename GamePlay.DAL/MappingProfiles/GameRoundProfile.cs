using AutoMapper;
using GamePlay.Domain.Entities;
using GamePlay.Domain.Models.GameRound;

namespace GamePlay.DAL.MappingProfiles;

public class GameRoundProfile : Profile
{
    public GameRoundProfile()
    {
        CreateMap<GameRoundModel, GameRound>();
        CreateMap<GameRound, GameRoundModel>();
        CreateMap<CreateGameRoundModel, GameRound>();
    }
}