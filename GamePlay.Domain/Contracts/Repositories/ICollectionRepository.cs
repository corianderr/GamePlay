using GamePlay.Domain.Entities;

namespace GamePlay.Domain.Contracts.Repositories;

public interface ICollectionRepository : IBaseRepository<Collection>
{
    Task AddGameAsync(Game game, Guid collectionId);
    Task DeleteGameAsync(Game game, Guid collectionId);
}