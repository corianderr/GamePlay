using AutoMapper;
using GamePlay.Domain.Contracts.Repositories;
using GamePlay.Domain.Contracts.Services;
using GamePlay.Domain.Entities;
using GamePlay.Domain.Models;
using GamePlay.Domain.Models.Game;

namespace GamePlay.BLL.Services;

public class GameRatingService : IGameRatingService
{
    private readonly IGameRatingRepository _ratingRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IMapper _mapper;

    public GameRatingService(IMapper mapper, IGameRatingRepository ratingRepository, IGameRepository gameRepository)
    {
        _ratingRepository = ratingRepository;
        _gameRepository = gameRepository;
        _mapper = mapper;
    }

    public async Task<BaseModel> AddAsync(CreateGameRatingModel entity)
    {
        var gameRating = _mapper.Map<GameRating>(entity);
        var numberOfRatings = _ratingRepository.GetGameRatingsCount(r => r.GameId.Equals(entity.GameId));
        var game = await _gameRepository.GetFirstAsync(g => g.Id.Equals(entity.GameId));
        game.AverageRating = (game.AverageRating * numberOfRatings + entity.Rating) / (numberOfRatings + 1);

        return new BaseModel
        {
            Id = (await _ratingRepository.AddAsync(gameRating)).Id
        };
    }

    public async Task<GameRatingModel> GetByUserAndGameAsync(string userId, Guid gameId)
    {
        var rating = await _ratingRepository.GetFirstAsync(r => r.GameId.Equals(gameId) && r.UserId.Equals(userId));
        return _mapper.Map<GameRatingModel>(rating);
    }
    
    public async Task<BaseModel> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var rating = await _ratingRepository.GetFirstAsync(g => g.Id.Equals(id));
        var game = await _gameRepository.GetFirstAsync(g => g.Id.Equals(rating.GameId));
        var numberOfRatings = _ratingRepository.GetGameRatingsCount(r => r.GameId.Equals(game.Id));
        game.AverageRating = (game.AverageRating * numberOfRatings - rating.Rating) / (numberOfRatings - 1);
        
        return new BaseModel
        {
            Id = (await _ratingRepository.DeleteAsync(rating)).Id
        };
    }

    public async Task<GameRatingModel> GetByIdAsync(Guid id)
    {
        var rating = await _ratingRepository.GetFirstAsync(r => r.Id.Equals(id));
        return _mapper.Map<GameRatingModel>(rating);
    }
}