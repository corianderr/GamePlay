using System.Linq.Expressions;
using GamePlay.DAL.Data;
using GamePlay.Domain.Contracts.Repositories;
using GamePlay.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamePlay.DAL.Repositories;

public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
{
    private readonly DbSet<UserRelation> DbRelationsSet;

    public UserRepository(ApplicationDbContext context) : base(context)
    {
        DbRelationsSet = Context.Set<UserRelation>();
    }

    public async Task AddGameAsync(Guid gameId, string userId)
    {
        var user = await GetFirstAsync(u => u.Id.Equals(userId));
        var game = await Context.Games.FirstOrDefaultAsync(g => g.Id.Equals(gameId));
        user.Games.Add(game);
        await Context.SaveChangesAsync();
    }

    public async Task<UserRelation> AddSubscriptionAsync(UserRelation entity)
    {
        var addedEntity = (await DbRelationsSet.AddAsync(entity)).Entity;

        // TODO: Try to delete Attach and Entry lines
        var user = await GetFirstAsync(u => u.Id.Equals(entity.UserId));
        DbSet.Attach(user);
        user.FollowersCount++;
        Context.Entry(user).Property(u => u.FollowersCount).IsModified = true;

        await Context.SaveChangesAsync();

        return addedEntity;
    }

    public async Task<UserRelation> BecomeFriendsAsync(ApplicationUser subscriber, ApplicationUser user)
    {
        var relation =
            await DbRelationsSet.FirstOrDefaultAsync(r =>
                r.SubscriberId.Equals(subscriber.Id) && r.UserId.Equals(user.Id));
        relation.IsFriend = true;

        await Context.SaveChangesAsync();
        return relation;
    }

    public async Task<IEnumerable<UserRelation>> GetAllRelationsAsync(
        Expression<Func<UserRelation, bool>>? predicate = null,
        params Expression<Func<UserRelation, object>>[] includeProperties)
    {
        IQueryable<UserRelation> query = DbRelationsSet;
        foreach (var includeProperty in includeProperties) query = query.Include(includeProperty);
        if (predicate != null) return await query.Where(predicate).ToListAsync();
        return await query.ToListAsync();
    }

    public async Task<UserRelation?> GetFirstRelationAsync(Expression<Func<UserRelation, bool>>? predicate = null,
        params Expression<Func<UserRelation, object>>[] includeProperties)
    {
        IQueryable<UserRelation> query = DbRelationsSet;
        foreach (var includeProperty in includeProperties) query = query.Include(includeProperty);
        if (predicate != null) return await query.FirstOrDefaultAsync(predicate);
        return await query.FirstOrDefaultAsync();
    }

    public bool IsEmailUnique(string email)
    {
        ApplicationUser? user = null;
        user = DbSet.FirstOrDefault(u => u.Email.Equals(email));
        return user == default;
    }

    public bool IsUsernameUnique(string username)
    {
        ApplicationUser? user = null;
        user = DbSet.FirstOrDefault(u => u.UserName.Equals(username));
        return user == default;
    }
}