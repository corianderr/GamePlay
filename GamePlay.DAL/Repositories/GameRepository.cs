using GamePlay.DAL.Data;
using GamePlay.Domain.Contracts;
using GamePlay.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamePlay.DAL.Repositories;

public class GameRepository : BaseRepository<Game>, IGameRepository
{
    protected GameRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<GameRating> AddRatingAsync(GameRating entity)
    {
        var addedEntity = (await Context.GameRatings!.AddAsync(entity)).Entity;
        var numberOfRatings = Context.GameRatings.Count(r => r.GameId.Equals(entity.GameId));
        var game = await GetFirstAsync(g => g.Id.Equals(entity.GameId));
        
        DbSet.Attach(game);
        game.AverageRating = (game.AverageRating * numberOfRatings + entity.Rating) / numberOfRatings + 1;
        Context.Entry(game).Property(g => g.AverageRating).IsModified = true;
        
        await Context.SaveChangesAsync();
        return addedEntity;
    }

    public async Task<GameRating> GetRatingAsync(Guid userId, Guid gameId)
    {
        return await Context.GameRatings.FirstOrDefaultAsync(r => r.GameId.Equals(gameId) && r.UserId.Equals(userId));
    }
}