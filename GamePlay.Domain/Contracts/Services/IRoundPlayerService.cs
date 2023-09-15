using System.Linq.Expressions;
using GamePlay.Domain.Models;
using GamePlay.Domain.Models.RoundPlayer;

namespace GamePlay.Domain.Contracts.Services;

public interface IRoundPlayerService {
    Task<BaseModel> CreateAsync(CreateRoundPlayerModel createModel,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<RoundPlayerModel>> GetAllAsync(Expression<Func<RoundPlayerModel, bool>>? predicate = null);
}