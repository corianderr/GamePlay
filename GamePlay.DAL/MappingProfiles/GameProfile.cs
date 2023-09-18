using AutoMapper;
using GamePlay.Domain.Entities;
using GamePlay.Domain.Models.Game;
using GamePlay.Domain.Models.GameRating;

namespace GamePlay.DAL.MappingProfiles;

public class GameProfile : Profile {
    public GameProfile() {
        CreateMap<CreateGameModel, Game>()
            .ForMember(g => g.AverageRating, map => map.MapFrom(g => 0));
        CreateMap<UpdateGameModel, Game>();
        CreateMap<Game, GameModel>();
        CreateMap<GameModel, Game>();
        CreateMap<CreateGameRatingModel, GameRating>();
        CreateMap<GameRating, GameRatingModel>();
    }
}