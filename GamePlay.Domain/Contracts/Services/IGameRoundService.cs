using System.Linq.Expressions;
using GamePlay.Domain.Entities;
using GamePlay.Domain.Models;
using GamePlay.Domain.Models.GameRound;

namespace GamePlay.Domain.Contracts.Services;

public interface IGameRoundService
{
    Task<BaseModel> AddAsync(CreateGameRoundModel entity);
    Task<IEnumerable<GameRoundModel>> GetAllByGameIdAsync(Guid gameId);
    Task<GameRoundModel> GetByIdAsync(Guid id);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task UpdateAsync(Guid id, GameRoundModel updateModel,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<GameRoundModel>> GetAllAsync(Expression<Func<GameRoundModel, bool>>? predicate = null);
    Task<IEnumerable<string?>> GetDistinctPlacesAsync();
    Task<IEnumerable<string>> GetDistinctPlayersAsync();
}