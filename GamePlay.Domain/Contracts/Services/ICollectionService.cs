using System.Linq.Expressions;
using GamePlay.Domain.Models;
using GamePlay.Domain.Models.Collection;
using GamePlay.Domain.Models.Game;

namespace GamePlay.Domain.Contracts.Services;

public interface ICollectionService
{
    Task<CollectionModel>
        GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<CollectionModel>> GetAllAsync(Expression<Func<CollectionModel, bool>>? predicate = null);

    Task<BaseModel> CreateAsync(CreateCollectionModel createCollectionModel,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    // TODO: In API version return UpdateGameModel type
    Task UpdateAsync(Guid id, CollectionModel updateCollectionModel,
        CancellationToken cancellationToken = default);
    Task AddGameAsync(Guid gameId, Guid collectionId);
    Task DeleteGameAsync(Guid gameId, Guid collectionId);
    Task<IEnumerable<CollectionModel>> GetAllWhereMissing(Guid gameId);

}