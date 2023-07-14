using System.Linq.Expressions;
using GamePlay.DAL.Data;
using GamePlay.Domain.Contracts;
using GamePlay.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamePlay.DAL.Repositories;

public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
{
    protected UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async void AddGameAsync(Guid gameId, Guid userId)
    {
        var user = await GetFirstAsync(u => u.Id.Equals(userId));
        var game = await Context.Games.FirstOrDefaultAsync(g => g.Id.Equals(gameId));
        user.Games.Add(game);
        await Context.SaveChangesAsync();
    }

    public async Task<UserRelation> SubscribeAsync(UserRelation entity)
    {
        throw new NotImplementedException();
    }

    public async Task<UserRelation> BecomeFriendsAsync(UserRelation entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<UserRelation>> GetAllRelationsAsync(Expression<Func<UserRelation, bool>> predicate, params Expression<Func<UserRelation, object>>[] includeProperties)
    {
        throw new NotImplementedException();
    }
}