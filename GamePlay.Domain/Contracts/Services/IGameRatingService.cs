using GamePlay.Domain.Models;
using GamePlay.Domain.Models.Game;

namespace GamePlay.Domain.Contracts.Services;

public interface IGameRatingService
{
    Task<BaseModel> AddAsync(CreateGameRatingModel entity);
    Task<GameRatingModel> GetAsync(string userId, Guid gameId);
}