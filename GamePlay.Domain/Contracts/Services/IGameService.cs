using System.Linq.Expressions;
using GamePlay.Domain.Models;
using GamePlay.Domain.Models.Game;

namespace GamePlay.Domain.Contracts.Services;

public interface IGameService
{
    Task<GameModel>
        GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<BaseModel> CreateAsync(CreateGameModel createGameModel,
        CancellationToken cancellationToken = default);

    Task<BaseModel> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    // TODO: In API version return UpdateGameModel type
    Task<BaseModel> UpdateAsync(Guid id, GameModel updateGameModel,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<GameModel>> GetAllAsync(Expression<Func<GameModel, bool>>? predicate = null);
    
    // TODO: Fix to collections implementation
    // Task<bool> CheckIfTheUserHas(string userId, Guid gameId);
}