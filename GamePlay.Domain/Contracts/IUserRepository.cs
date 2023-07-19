using System.Linq.Expressions;
using GamePlay.Domain.Entities;

namespace GamePlay.Domain.Contracts;

public interface IUserRepository : IBaseRepository<ApplicationUser>
{
    bool IsEmailUnique(string email);
    public bool IsUsernameUnique(string username);
    Task AddGameAsync(Guid gameId, string userId);
    Task<UserRelation> AddSubscriptionAsync(UserRelation entity);
    Task<UserRelation> BecomeFriendsAsync(string subscriberId, string userId);
    Task<IEnumerable<UserRelation>> GetAllRelationsAsync(Expression<Func<UserRelation, bool>> predicate, params Expression<Func<UserRelation, object>>[] includeProperties);
}