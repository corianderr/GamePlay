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

    // TODO: Fix to collections implementation
    // public async Task AddGameAsync(Guid gameId, string userId)
    // {
    //     var user = await GetFirstAsync(u => u.Id.Equals(userId));
    //     var game = await Context.Games.FirstOrDefaultAsync(g => g.Id.Equals(gameId));
    //     user.Games.Add(game);
    //     await Context.SaveChangesAsync();
    // }
    
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