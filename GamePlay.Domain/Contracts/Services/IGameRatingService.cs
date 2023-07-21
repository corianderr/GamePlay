using GamePlay.Domain.Models;
using GamePlay.Domain.Models.Game;

namespace GamePlay.Domain.Contracts.Services;

public interface IGameRatingService
{
    Task<BaseModel> AddAsync(CreateGameRatingModel entity);
    Task<GameRatingModel> GetByUserAndGameAsync(string userId, Guid gameId);
    Task<GameRatingModel> GetByIdAsync(Guid id);
    Task<BaseModel> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}