using System.Linq.Expressions;
using GamePlay.DAL.Data;
using GamePlay.Domain.Contracts.Repositories;
using GamePlay.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamePlay.DAL.Repositories;

public class GameRepository : BaseRepository<Game>, IGameRepository
{
    public GameRepository(ApplicationDbContext context) : base(context)
    {
    }
}