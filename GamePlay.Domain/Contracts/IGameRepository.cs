using GamePlay.Domain.Entities;

namespace GamePlay.Domain.Contracts;

public interface IGameRepository : IBaseRepository<Game>
{
    Task<GameRating> AddRatingAsync(GameRating entity);
    Task<GameRating> GetRatingAsync(Guid userId, Guid gameId);
}