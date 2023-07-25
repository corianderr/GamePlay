using GamePlay.DAL.Data;
using GamePlay.Domain.Contracts.Repositories;
using GamePlay.Domain.Entities;

namespace GamePlay.DAL.Repositories;

public class GameRoundRepository : BaseRepository<GameRound>, IGameRoundRepository
{
    public GameRoundRepository(ApplicationDbContext context) : base(context)
    {
    }
}