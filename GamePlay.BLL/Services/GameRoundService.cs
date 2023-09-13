using System.Collections;
using System.Linq.Expressions;
using AutoMapper;
using GamePlay.Domain.Contracts.Repositories;
using GamePlay.Domain.Contracts.Services;
using GamePlay.Domain.Entities;
using GamePlay.Domain.Models;
using GamePlay.Domain.Models.GameRound;

namespace GamePlay.BLL.Services;

public class GameRoundService : IGameRoundService {
    private readonly IGameRoundRepository _gameRoundRepository;
    private readonly IRoundPlayerRepository _roundPlayerRepository;
    private readonly IMapper _mapper;

    public GameRoundService(IGameRoundRepository gameRoundRepository, IMapper mapper,
        IRoundPlayerRepository roundPlayerRepository) {
        _gameRoundRepository = gameRoundRepository;
        _mapper = mapper;
        _roundPlayerRepository = roundPlayerRepository;
    }

    public async Task<BaseModel> AddAsync(CreateGameRoundModel entity) {
        var gameRound = _mapper.Map<GameRound>(entity);
        var gameRoundId = (await _gameRoundRepository.AddAsync(gameRound)).Id;
        return new BaseModel() {
            Id = gameRoundId
        };
    }

    public async Task<IEnumerable<GameRoundModel>> GetAllByGameIdAsync(Guid gameId) {
        var rounds = await _gameRoundRepository.GetAllAsync(r => r.GameId.Equals(gameId), r => r.Game);
        return _mapper.Map<IEnumerable<GameRoundModel>>(rounds);
    }

    public async Task<IEnumerable<GameRoundModel>> GetAllAsync(Expression<Func<GameRoundModel, bool>>? predicate = null) {
        var rounds = await _gameRoundRepository.GetAllAsync(_mapper.Map<Expression<Func<GameRound, bool>>?>(predicate),
            r => r.Game);
        return _mapper.Map<IEnumerable<GameRoundModel>>(rounds);
    }

    public async Task<IEnumerable<string?>> GetDistinctPlacesAsync() {
        return await _gameRoundRepository.GetDistinctPlacesAsync();
    }

    public async Task<IEnumerable<string>> GetDistinctPlayersAsync() {
        return await _gameRoundRepository.GetDistinctPlayersAsync();
    }

    public async Task<GameRoundModel> GetByIdAsync(Guid id) {
        var round = await _gameRoundRepository.GetFirstAsync(r => r.Id.Equals(id), r => r.Players, r => r.Game);
        return _mapper.Map<GameRoundModel>(round);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default) {
        var round = await _gameRoundRepository.GetFirstAsync(r => r.Id.Equals(id));
        await _gameRoundRepository.DeleteAsync(round);
    }

    public async Task UpdateAsync(Guid id, GameRoundModel updateModel,
        CancellationToken cancellationToken = default) {
        var round = await _gameRoundRepository.GetFirstAsync(r => r.Id.Equals(id), r => r.Players);
        _mapper.Map(updateModel, round);
        await _gameRoundRepository.UpdateAsync(round);
    }
}