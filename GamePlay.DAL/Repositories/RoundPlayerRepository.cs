using GamePlay.DAL.Data;
using GamePlay.Domain.Contracts.Repositories;
using GamePlay.Domain.Entities;

namespace GamePlay.DAL.Repositories;

public class RoundPlayerRepository : BaseRepository<RoundPlayer>, IRoundPlayerRepository {
    public RoundPlayerRepository(ApplicationDbContext context) : base(context) {
    }
}