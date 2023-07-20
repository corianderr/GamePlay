using System.Linq.Expressions;
using GamePlay.DAL.Data;
using GamePlay.Domain.Contracts.Repositories;
using GamePlay.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamePlay.DAL.Repositories;

public class GameRepository : BaseRepository<Game>, IGameRepository
{
    public GameRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<GameRating> AddRatingAsync(GameRating entity)
    {
        var addedEntity = (await Context.GameRatings!.AddAsync(entity)).Entity;

        await Context.SaveChangesAsync();
        return addedEntity;
    }

    public int GetGameRatingsCount(Expression<Func<GameRating, bool>>? predicate = null)
    {
        return Context.GameRatings.Count(predicate);
    }

    public async Task<GameRating> GetRatingAsync(string userId, Guid gameId)
    {
        return await Context.GameRatings.FirstOrDefaultAsync(r => r.GameId.Equals(gameId) && r.UserId.Equals(userId));
    }
}