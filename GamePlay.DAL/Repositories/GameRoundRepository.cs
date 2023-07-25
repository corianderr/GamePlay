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

    public async Task<IEnumerable<string?>> GetDistinctPlacesAsync(string userId)
    {
        return await DbSet.Select(r => r.Place).Distinct().ToListAsync();
    }
}