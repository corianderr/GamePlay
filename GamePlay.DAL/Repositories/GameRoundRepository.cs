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

    public async Task<IEnumerable<string?>> GetDistinctPlacesAsync()
    {
        return await DbSet.Select(r => r.Place).Distinct().ToListAsync();
    }

    public async Task<IEnumerable<string>> GetDistinctPlayersAsync()
    {
        return await DbSet.SelectMany(r => r.Players).Select(p => p.Name).Distinct().ToListAsync();
    }
}