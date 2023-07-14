using System.Linq.Expressions;
using GamePlay.Domain.Entities;

namespace GamePlay.Domain.Contracts;

public interface IUserRepository : IBaseRepository<ApplicationUser>
{
    void AddGameAsync(Guid gameId, Guid userId);
    Task<UserRelation> AddSubscriptionAsync(UserRelation entity);
    Task<UserRelation> BecomeFriendsAsync(Guid relationsId);
    Task<IEnumerable<UserRelation>> GetAllRelationsAsync(Expression<Func<UserRelation, bool>> predicate, params Expression<Func<UserRelation, object>>[] includeProperties);
}