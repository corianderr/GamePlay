using System.Linq.Expressions;
using AutoMapper;
using GamePlay.BLL.Services.Interfaces;
using GamePlay.Domain.Contracts;
using GamePlay.Domain.Entities;
using GamePlay.Domain.Models;
using GamePlay.Domain.Models.Game;

namespace GamePlay.BLL.Services;

public class GameService : IGameService
{
    private readonly IMapper _mapper;
    private readonly IGameRepository _gameRepository;

    public GameService(IMapper mapper, IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
        _mapper = mapper;
    }
    
    public async Task<BaseResponseModel> AddRatingAsync(CreateGameRatingModel entity)
    {
        var gameRating = _mapper.Map<GameRating>(entity);
        return new BaseResponseModel
        {
            Id = (await _gameRepository.AddRatingAsync(gameRating)).Id
        };
    }

    public async Task<GameRatingResponseModel> GetRatingAsync(string userId, Guid gameId)
    {
        var rating = await _gameRepository.GetRatingAsync(userId, gameId);
        return _mapper.Map<GameRatingResponseModel>(rating);
    }

    public async Task<GameResponseModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var game = await _gameRepository.GetFirstAsync(g => g.Id.Equals(id));
        return _mapper.Map<GameResponseModel>(game);
    }

    public async Task<BaseResponseModel> CreateAsync(CreateGameModel createGameModel, CancellationToken cancellationToken = default)
    {
        var isExist = (await _gameRepository.GetFirstAsync(g => g.Name.Equals(createGameModel.Name))) != null;
        if (isExist) 
            throw new ArgumentException("The game already exists, but you can create another one :)");
        
        var game = _mapper.Map<Game>(createGameModel);;
        return new BaseResponseModel
        {
            Id = (await _gameRepository.AddAsync(game)).Id
        };
    }

    public async Task<BaseResponseModel> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var game = await _gameRepository.GetFirstAsync(g => g.Id.Equals(id));
        return new BaseResponseModel
        {
            Id = (await _gameRepository.DeleteAsync(game)).Id
        };
    }

    public async Task<BaseResponseModel> UpdateAsync(Guid id, GameResponseModel updateGameModel, CancellationToken cancellationToken = default)
    {
        var game = await _gameRepository.GetFirstAsync(e => e.Id == id);
        _mapper.Map(updateGameModel, game);
        return new BaseResponseModel
        {
            Id = (await _gameRepository.UpdateAsync(game)).Id
        };
    }

    public async Task<IEnumerable<GameResponseModel>> GetAllAsync(Expression<Func<GameResponseModel, bool>>? predicate = null)
    {
        var games = await _gameRepository.GetAllAsync(_mapper.Map<Expression<Func<Game, bool>>>(predicate));
        return _mapper.Map<IEnumerable<GameResponseModel>>(games);
    }
}