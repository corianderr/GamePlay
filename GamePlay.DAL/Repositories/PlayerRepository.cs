using GamePlay.DAL.Data;
using GamePlay.Domain.Contracts.Repositories;
using GamePlay.Domain.Entities;

namespace GamePlay.DAL.Repositories;

public class PlayerRepository : BaseRepository<Player>, IPlayerRepository
{
    protected PlayerRepository(ApplicationDbContext context) : base(context)
    {
    }
}