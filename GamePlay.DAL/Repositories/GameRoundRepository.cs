using System.Linq.Expressions;
using GamePlay.DAL.Data;
using GamePlay.Domain.Contracts.Repositories;
using GamePlay.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamePlay.DAL.Repositories;

public class GameRoundRepository : BaseRepository<GameRound>, IGameRoundRepository
{
    public GameRoundRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<object?>> GetDistinctColumnAsync(Expression<Func<GameRound, object>> column)
    {
        return await DbSet.Select(column).Distinct().ToListAsync();
    }
}