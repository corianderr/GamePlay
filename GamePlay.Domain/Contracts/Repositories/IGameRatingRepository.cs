using System.Linq.Expressions;
using GamePlay.Domain.Entities;

namespace GamePlay.Domain.Contracts.Repositories;

public interface IGameRatingRepository : IBaseRepository<GameRating>
{
    int GetGameRatingsCount(Expression<Func<GameRating, bool>>? predicate = null);
}