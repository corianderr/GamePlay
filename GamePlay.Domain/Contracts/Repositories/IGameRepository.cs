using System.Linq.Expressions;
using GamePlay.Domain.Entities;

namespace GamePlay.Domain.Contracts.Repositories;

public interface IGameRepository : IBaseRepository<Game>
{
    Task<GameRating> AddRatingAsync(GameRating entity);
    Task<GameRating> GetRatingAsync(string userId, Guid gameId);
    int GetGameRatingsCount(Expression<Func<GameRating, bool>>? predicate = null);
}