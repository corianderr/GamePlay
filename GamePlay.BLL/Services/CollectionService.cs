using System.Linq.Expressions;
using AutoMapper;
using GamePlay.Domain.Contracts.Repositories;
using GamePlay.Domain.Contracts.Services;
using GamePlay.Domain.Entities;
using GamePlay.Domain.Exceptions;
using GamePlay.Domain.Models;
using GamePlay.Domain.Models.Collection;
using GamePlay.Domain.Models.Game;

namespace GamePlay.BLL.Services;

public class CollectionService : ICollectionService
{
    private readonly IMapper _mapper;
    private readonly ICollectionRepository _collectionRepository;
    private readonly IGameRepository _gameRepository;

    public CollectionService(IMapper mapper, ICollectionRepository collectionRepository, IGameRepository gameRepository)
    {
        _mapper = mapper;
        _collectionRepository = collectionRepository;
        _gameRepository = gameRepository;
    }

    public async Task<CollectionModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var collection = await _collectionRepository.GetFirstAsync(g => g.Id.Equals(id), c => c.Games);
        return _mapper.Map<CollectionModel>(collection);
    }

    public async Task<IEnumerable<CollectionModel>> GetAllAsync(
        Expression<Func<CollectionModel, bool>>? predicate = null)
    {
        var collections =
            await _collectionRepository.GetAllAsync(_mapper.Map<Expression<Func<Collection, bool>>>(predicate));
        return _mapper.Map<IEnumerable<CollectionModel>>(collections);
    }

    public async Task<BaseModel> CreateAsync(CreateCollectionModel createCollectionModel,
        CancellationToken cancellationToken = default)
    {
        var isExist = await _collectionRepository.GetFirstAsync(g =>
            g.Name.Equals(createCollectionModel.Name) && g.UserId.Equals(createCollectionModel.UserId)) != null;
        if (isExist)
            throw new ArgumentException(
                "The collection with that name already exists, but you can create another one :)");

        var collection = _mapper.Map<Collection>(createCollectionModel);
        return new BaseModel
        {
            Id = (await _collectionRepository.AddAsync(collection)).Id
        };
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var collection = await _collectionRepository.GetFirstAsync(g => g.Id.Equals(id));
        if (collection.IsDefault) throw new BadRequestException("Default collections can not be deleted!");
        await _collectionRepository.DeleteAsync(collection);
    }

    public async Task UpdateAsync(Guid id, CollectionModel updateCollectionModel,
        CancellationToken cancellationToken = default)
    {
        var collection = await _collectionRepository.GetFirstAsync(g => g.Id.Equals(id));
        _mapper.Map(updateCollectionModel, collection);
        await _collectionRepository.UpdateAsync(collection);
    }

    public async Task AddGameAsync(Guid gameId, Guid collectionId)
    {
        var game = await _gameRepository.GetFirstAsync(g => g.Id.Equals(gameId));
        await _collectionRepository.AddGameAsync(game, collectionId);
    }

    public async Task DeleteGameAsync(Guid gameId, Guid collectionId)
    {
        var game = await _gameRepository.GetFirstAsync(g => g.Id.Equals(gameId));
        await _collectionRepository.DeleteGameAsync(game, collectionId);
    }

    public async Task<IEnumerable<CollectionModel>> GetAllWhereMissing(string userId, Guid gameId)
    {
        var collections =
            await _collectionRepository.GetAllAsync(c =>
                !c.Games.Any(g => g.Id.Equals(gameId)) && c.UserId.Equals(userId));
        return _mapper.Map<IEnumerable<CollectionModel>>(collections);
    }
}