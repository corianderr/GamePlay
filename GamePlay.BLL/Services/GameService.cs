using System.Linq.Expressions;
using AutoMapper;
using GamePlay.Domain.Contracts.Repositories;
using GamePlay.Domain.Contracts.Services;
using GamePlay.Domain.Entities;
using GamePlay.Domain.Models;
using GamePlay.Domain.Models.Game;

namespace GamePlay.BLL.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;
    private readonly IMapper _mapper;

    public GameService(IMapper mapper, IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
        _mapper = mapper;
    }
    
    public async Task<GameModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var game = await _gameRepository.GetFirstAsync(g => g.Id.Equals(id));
        return _mapper.Map<GameModel>(game);
    }

    public async Task<BaseModel> CreateAsync(CreateGameModel createGameModel,
        CancellationToken cancellationToken = default)
    {
        var isExist = await _gameRepository.GetFirstAsync(g => g.Name.Equals(createGameModel.Name)) != null;
        if (isExist)
            throw new ArgumentException("The game already exists, but you can create another one :)");

        var game = _mapper.Map<Game>(createGameModel);
        ;
        return new BaseModel
        {
            Id = (await _gameRepository.AddAsync(game)).Id
        };
    }

    public async Task<BaseModel> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var game = await _gameRepository.GetFirstAsync(g => g.Id.Equals(id));
        return new BaseModel
        {
            Id = (await _gameRepository.DeleteAsync(game)).Id
        };
    }

    public async Task<BaseModel> UpdateAsync(Guid id, GameModel updateGameModel,
        CancellationToken cancellationToken = default)
    {
        var game = await _gameRepository.GetFirstAsync(e => e.Id == id);
        _mapper.Map(updateGameModel, game);
        return new BaseModel
        {
            Id = (await _gameRepository.UpdateAsync(game)).Id
        };
    }

    public async Task<IEnumerable<GameModel>> GetAllAsync(Expression<Func<GameModel, bool>>? predicate = null)
    {
        var games = await _gameRepository.GetAllAsync(_mapper.Map<Expression<Func<Game, bool>>>(predicate));
        return _mapper.Map<IEnumerable<GameModel>>(games);
    }
    
    public async Task<bool> CheckIfTheUserHas(string userId, Guid gameId)
    {
        var users = (await _gameRepository.GetFirstAsync(g => g.Id.Equals(gameId), g => g.Users)).Users;
        return users.Exists(u => u.Id.Equals(userId));
    }
}