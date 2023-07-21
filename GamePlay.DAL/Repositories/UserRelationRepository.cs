using System.Linq.Expressions;
using GamePlay.DAL.Data;
using GamePlay.Domain.Contracts.Repositories;
using GamePlay.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamePlay.DAL.Repositories;

public class UserRelationRepository : BaseRepository<UserRelation>, IUserRelationRepository
{
    public UserRelationRepository(ApplicationDbContext context) : base(context)
    {
    }
    public async Task<UserRelation> BecomeFriendsAsync(ApplicationUser subscriber, ApplicationUser user)
    {
        var relation = await DbSet.FirstOrDefaultAsync(r =>
                r.SubscriberId.Equals(subscriber.Id) && r.UserId.Equals(user.Id));
        relation.IsFriend = true;

        await Context.SaveChangesAsync();
        return relation;
    }
}