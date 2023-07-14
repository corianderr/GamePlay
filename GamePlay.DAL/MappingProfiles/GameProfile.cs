using AutoMapper;
using GamePlay.Domain.Entities;
using GamePlay.Domain.Models.Game;

namespace GamePlay.DAL.MappingProfiles;

public class GameProfile : Profile
{
    public GameProfile()
    {
        CreateMap<CreateGameModel, Game>();
        CreateMap<UpdateGameModel, Game>();
        CreateMap<Game, GameResponseModel>();
        CreateMap<CreateGameRatingModel, GameRating>();
        CreateMap<GameRating, GameRatingResponseModel>();
    }
}