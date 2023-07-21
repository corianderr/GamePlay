using System.Linq.Expressions;
using GamePlay.DAL.Data;
using GamePlay.Domain.Contracts.Repositories;
using GamePlay.Domain.Entities;

namespace GamePlay.DAL.Repositories;

public class GameRatingRepository : BaseRepository<GameRating>, IGameRatingRepository
{
    protected GameRatingRepository(ApplicationDbContext context) : base(context)
    {
    }
    public int GetGameRatingsCount(Expression<Func<GameRating, bool>>? predicate = null)
    {
        return Context.GameRatings.Count(predicate);
    }
}