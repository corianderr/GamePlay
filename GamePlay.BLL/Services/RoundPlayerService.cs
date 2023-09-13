using System.Linq.Expressions;
using AutoMapper;
using GamePlay.Domain.Contracts.Repositories;
using GamePlay.Domain.Contracts.Services;
using GamePlay.Domain.Entities;
using GamePlay.Domain.Models;
using GamePlay.Domain.Models.Player;

namespace GamePlay.BLL.Services;

public class RoundPlayerService : IRoundPlayerService {
    private readonly IRoundPlayerRepository _roundPlayerRepository;
    private readonly IMapper _mapper;

    public RoundPlayerService(IRoundPlayerRepository roundPlayerRepository, IMapper mapper) {
        _roundPlayerRepository = roundPlayerRepository;
        _mapper = mapper;
    }

    public async Task<BaseModel> CreateAsync(CreateRoundPlayerModel createModel, CancellationToken cancellationToken = default) {
        var player = _mapper.Map<RoundPlayer>(createModel);
        return new BaseModel() {
            Id = (await _roundPlayerRepository.AddAsync(player)).Id
        };
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default) {
        var player = await _roundPlayerRepository.GetFirstAsync(p => p.Id.Equals(id));
        await _roundPlayerRepository.DeleteAsync(player);
    }

    public async Task<IEnumerable<RoundPlayerModel>> GetAllAsync(Expression<Func<RoundPlayerModel, bool>>? predicate = null) {
        var players = await _roundPlayerRepository.GetAllAsync(_mapper.Map<Expression<Func<RoundPlayer, bool>>>(predicate));
        return _mapper.Map<IEnumerable<RoundPlayerModel>>(players);
    }
}