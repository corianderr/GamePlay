using System.Linq.Expressions;
using GamePlay.Domain.Models;
using GamePlay.Domain.Models.Game;

namespace GamePlay.BLL.Services.Interfaces;

public interface IGameService
{
    Task<GameRatingResponseModel> AddRatingAsync(GameRatingResponseModel entity);
    Task<GameRatingResponseModel> GetRatingAsync(Guid userId, Guid gameId);

    Task<GameResponseModel>
        GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<BaseResponseModel> CreateAsync(CreateGameModel createGameModel,
        CancellationToken cancellationToken = default);

    Task<BaseResponseModel> DeleteAsync(int id, CancellationToken cancellationToken = default);

    Task<BaseResponseModel> UpdateAsync(int id, UpdateGameModel updateGameModel,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<GameResponseModel>> GetAllAsync(Expression<Func<GameResponseModel, bool>> predicate);
}