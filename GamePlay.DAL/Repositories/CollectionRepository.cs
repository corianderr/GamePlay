using System.Linq.Expressions;
using GamePlay.DAL.Data;
using GamePlay.Domain.Contracts.Repositories;
using GamePlay.Domain.Entities;

namespace GamePlay.DAL.Repositories;

public class CollectionRepository : BaseRepository<Collection>, ICollectionRepository
{
    public CollectionRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task AddGameAsync(Game game, Guid collectionId)
    {
        var collection = await GetFirstAsync(c => c.Id.Equals(collectionId));
        collection.Games.Add(game);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteGameAsync(Game game, Guid collectionId)
    {
        var collection = await GetFirstAsync(c => c.Id.Equals(collectionId));
        collection.Games.Remove(game);
        await Context.SaveChangesAsync();
    }
}