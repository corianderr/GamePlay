using GamePlay.Domain.Entities;

namespace GamePlay.Domain.Contracts;

public interface IGameRepository : IBaseRepository<Game>
{
    Task<GameRating> AddRating(GameRating entity);
    Task<GameRating> GetRating(Guid userId, Guid gameId);
}