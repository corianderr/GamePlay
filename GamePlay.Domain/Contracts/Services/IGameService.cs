using System.Linq.Expressions;
using GamePlay.Domain.Models;
using GamePlay.Domain.Models.Game;

namespace GamePlay.Domain.Contracts.Services;

public interface IGameService {
    Task<GameModel>
        GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<BaseModel> CreateAsync(CreateGameModel createGameModel,
        CancellationToken cancellationToken = default);

    Task<BaseModel> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<BaseModel> UpdateAsync(Guid id, UpdateGameModel updateGameModel,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<GameModel>> GetAllAsync(Expression<Func<GameModel, bool>>? predicate = null);
}