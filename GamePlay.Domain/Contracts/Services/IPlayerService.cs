using System.Linq.Expressions;
using GamePlay.Domain.Models;
using GamePlay.Domain.Models.Player;

namespace GamePlay.Domain.Contracts.Services;

public interface IPlayerService {
    Task<BaseModel> CreateAsync(CreatePlayerModel createModel,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<PlayerModel>> GetAllAsync(Expression<Func<PlayerModel, bool>>? predicate = null);
}