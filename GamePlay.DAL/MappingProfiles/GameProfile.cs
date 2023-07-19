using AutoMapper;
using GamePlay.Domain.Entities;
using GamePlay.Domain.Models.Game;

namespace GamePlay.DAL.MappingProfiles;

public class GameProfile : Profile
{
    public GameProfile()
    {
        CreateMap<CreateGameModel, Game>()
            .ForMember(g => g.AverageRating, map => map.MapFrom(g => 0));;
        CreateMap<UpdateGameModel, Game>();
        CreateMap<Game, GameResponseModel>();
        CreateMap<GameResponseModel, Game>();
        CreateMap<CreateGameRatingModel, GameRating>();
        CreateMap<GameRating, GameRatingResponseModel>();
    }
}