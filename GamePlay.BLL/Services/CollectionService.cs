using System.Linq.Expressions;
using AutoMapper;
using GamePlay.Domain.Contracts.Repositories;
using GamePlay.Domain.Contracts.Services;
using GamePlay.Domain.Entities;
using GamePlay.Domain.Models;
using GamePlay.Domain.Models.Collection;
using GamePlay.Domain.Models.Game;

namespace GamePlay.BLL.Services;

public class CollectionService : ICollectionService
{
    private readonly IMapper _mapper;
    private readonly ICollectionRepository _collectionRepository;

    public CollectionService(IMapper mapper, ICollectionRepository collectionRepository)
    {
        _mapper = mapper;
        _collectionRepository = collectionRepository;
    }

    public async Task<CollectionModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var collection = await _collectionRepository.GetFirstAsync(g => g.Id.Equals(id));
        return _mapper.Map<CollectionModel>(collection);
    }

    public async Task<IEnumerable<CollectionModel>> GetAllAsync(Expression<Func<CollectionModel, bool>>? predicate = null)
    {
        var collections = await _collectionRepository.GetAllAsync(_mapper.Map<Expression<Func<Collection, bool>>>(predicate));
        return _mapper.Map<IEnumerable<CollectionModel>>(collections);
    }

    public async Task<BaseModel> CreateAsync(CreateCollectionModel createCollectionModel, CancellationToken cancellationToken = default)
    {
        var isExist = await _collectionRepository.GetFirstAsync(g => g.Name.Equals(createCollectionModel.Name) && g.UserId.Equals(createCollectionModel.UserId)) != null;
        if (isExist)
            throw new ArgumentException("The collection already exists, but you can create another one :)");

        var collection = _mapper.Map<Collection>(createCollectionModel);
        return new BaseModel
        {
            Id = (await _collectionRepository.AddAsync(collection)).Id
        };
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Guid id, CollectionModel updateGameModel, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<GameModel>> GetGamesByIdAsync(Guid collectionId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task AddGameAsync(Guid gameId, Guid collectionId)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteGameAsync(Guid gameId, Guid collectionId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<CollectionModel>> GetAllWhereMissing(Guid gameId)
    {
        throw new NotImplementedException();
    }
}