using System.Linq.Expressions;
using AutoMapper;
using GamePlay.Domain.Contracts.Repositories;
using GamePlay.Domain.Contracts.Services;
using GamePlay.Domain.Entities;
using GamePlay.Domain.Models;
using GamePlay.Domain.Models.Player;

namespace GamePlay.BLL.Services; 

public class PlayerService : IPlayerService {
    private readonly IPlayerRepository _playerRepository;
    private readonly IMapper _mapper;

    public PlayerService(IPlayerRepository playerRepository, IMapper mapper) {
        _playerRepository = playerRepository;
        _mapper = mapper;
    }

    public async Task<BaseModel> CreateAsync(CreatePlayerModel createModel, CancellationToken cancellationToken = default) {
        if ((await _playerRepository.GetAllAsync()).Any(p => p.Name.Equals(createModel.Name))) {
            throw new ArgumentException("The player with that name already exists, but you can create another one :)");
        }
        var player = _mapper.Map<Player>(createModel);
        return new BaseModel() {
            Id = (await _playerRepository.AddAsync(player)).Id
        };
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default) {
        var player = await _playerRepository.GetFirstAsync(p => p.Id.Equals(id));
        await _playerRepository.DeleteAsync(player);
    }

    public async Task<IEnumerable<PlayerModel>> GetAllAsync(Expression<Func<PlayerModel, bool>>? predicate = null) {
        var players = await _playerRepository.GetAllAsync(_mapper.Map<Expression<Func<Player, bool>>>(predicate));
        return _mapper.Map<IEnumerable<PlayerModel>>(players);
    }
}