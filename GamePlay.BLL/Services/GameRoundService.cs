using AutoMapper;
using GamePlay.Domain.Contracts.Repositories;
using GamePlay.Domain.Contracts.Services;
using GamePlay.Domain.Entities;
using GamePlay.Domain.Models;
using GamePlay.Domain.Models.GameRound;

namespace GamePlay.BLL.Services;

public class GameRoundService : IGameRoundService
{
    private readonly IGameRoundRepository _gameRoundRepository;
    private readonly IMapper _mapper;

    public GameRoundService(IGameRoundRepository gameRoundRepository, IMapper mapper)
    {
        _gameRoundRepository = gameRoundRepository;
        _mapper = mapper;
    }
    
    public async Task<BaseModel> AddAsync(CreateGameRoundModel entity)
    {
        var gameRound = _mapper.Map<GameRound>(entity);
        return new BaseModel()
        {
            Id = (await _gameRoundRepository.AddAsync(gameRound)).Id
        };
    }

    public async Task<IEnumerable<GameRoundModel>> GetAllByGameIdAsync(Guid gameId)
    {
        var rounds = await _gameRoundRepository.GetAllAsync(r => r.GameId.Equals(gameId));
        return _mapper.Map<IEnumerable<GameRoundModel>>(rounds);
    }

    public async Task<GameRoundModel> GetByIdAsync(Guid id)
    {
        var round = await _gameRoundRepository.GetFirstAsync(r => r.Id.Equals(id), r => r.Players);
        return _mapper.Map<GameRoundModel>(round);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var round = await _gameRoundRepository.GetFirstAsync(r => r.Id.Equals(id));
        await _gameRoundRepository.DeleteAsync(round);
    }

    public async Task UpdateAsync(Guid id, CreateGameRoundModel updateModel, CancellationToken cancellationToken = default)
    {
        var round = await _gameRoundRepository.GetFirstAsync(r => r.Id.Equals(id));
        _mapper.Map(updateModel, round);
        await _gameRoundRepository.UpdateAsync(round);
    }
}