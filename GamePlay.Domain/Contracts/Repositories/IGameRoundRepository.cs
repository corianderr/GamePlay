using System.Linq.Expressions;
using GamePlay.Domain.Entities;

namespace GamePlay.Domain.Contracts.Repositories;

public interface IGameRoundRepository : IBaseRepository<GameRound>
{
    Task<IEnumerable<string?>> GetDistinctPlacesAsync();
    Task<IEnumerable<string>> GetDistinctPlayersAsync();
}