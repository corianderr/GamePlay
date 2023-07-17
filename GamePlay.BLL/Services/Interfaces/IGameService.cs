using System.Linq.Expressions;
using GamePlay.Domain.Models;
using GamePlay.Domain.Models.Game;

namespace GamePlay.BLL.Services.Interfaces;

public interface IGameService
{
    Task<BaseResponseModel> AddRatingAsync(GameRatingResponseModel entity);
    Task<GameRatingResponseModel> GetRatingAsync(Guid userId, Guid gameId);

    Task<GameResponseModel>
        GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<BaseResponseModel> CreateAsync(CreateGameModel createGameModel,
        CancellationToken cancellationToken = default);

    Task<BaseResponseModel> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<BaseResponseModel> UpdateAsync(Guid id, UpdateGameModel updateGameModel,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<GameResponseModel>> GetAllAsync(Expression<Func<GameResponseModel, bool>> predicate);
}