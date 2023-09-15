using System.Linq.Expressions;
using GamePlay.DAL.Data;
using GamePlay.Domain.Contracts.Repositories;
using GamePlay.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamePlay.DAL.Repositories;

public class GameRoundRepository : BaseRepository<GameRound>, IGameRoundRepository {
    public GameRoundRepository(ApplicationDbContext context) : base(context) {
    }

    public async Task<IEnumerable<string?>> GetDistinctPlacesAsync() {
        return await DbSet.Select(r => r.Place).Distinct().ToListAsync();
    }

    public async Task<GameRound> GetRoundWithAllHierarchy(Expression<Func<GameRound, bool>>? predicate = null) {
        IQueryable<GameRound> query = DbSet;
        query = query.Include(r => r.Game).Include(r => r.Players).ThenInclude(p => p.Player);

        if (predicate != null) {
            return (await query.FirstOrDefaultAsync(predicate))!;
        }

        return (await query.FirstOrDefaultAsync())!;
    }
}