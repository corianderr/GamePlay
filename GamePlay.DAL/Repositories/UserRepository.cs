using System.Linq.Expressions;
using GamePlay.DAL.Data;
using GamePlay.Domain.Contracts;
using GamePlay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

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
        await Context.SaveChangesAsync();

        return addedEntity;
    }

    public async Task<UserRelation> BecomeFriendsAsync(string subscriberId, string userId)
    {
        var relation = await DbRelationsSet.FirstOrDefaultAsync(r => r.SubscriberId.Equals(subscriberId) && r.UserId.Equals(userId));
        
        DbRelationsSet.Attach(relation);
        relation.IsFriend = true;
        Context.Entry(relation).Property(g => g.IsFriend).IsModified = true;
        
        await Context.SaveChangesAsync();
        return relation;
    }

    public async Task<IEnumerable<UserRelation>> GetAllRelationsAsync(Expression<Func<UserRelation, bool>>? predicate = null, params Expression<Func<UserRelation, object>>[] includeProperties)
    {
        IQueryable<UserRelation> query = DbRelationsSet;
        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }
        if (predicate != null) return await query.Where(predicate).ToListAsync();
        return await query.ToListAsync();
    }
    
    public async Task<UserRelation?> GetFirstRelationAsync(Expression<Func<UserRelation, bool>>? predicate = null, params Expression<Func<UserRelation, object>>[] includeProperties)
    {
        IQueryable<UserRelation> query = DbRelationsSet;
        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }
        if (predicate != null) return await query.FirstOrDefaultAsync(predicate);
        return await query.FirstOrDefaultAsync();
    }
    
    public bool IsEmailUnique (string email)
    {
        ApplicationUser? user = null;
        user = DbSet.FirstOrDefault(u => u.Email.Equals(email));
        return user == default;
    }

    public bool IsUsernameUnique (string username) {
        ApplicationUser? user = null;
        user = DbSet.FirstOrDefault(u => u.UserName.Equals(username));
        return user == default;
    }
}