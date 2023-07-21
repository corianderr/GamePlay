using System.Linq.Expressions;
using GamePlay.Domain.Entities;

namespace GamePlay.Domain.Contracts.Repositories;

public interface IUserRelationRepository : IBaseRepository<UserRelation>
{
    Task<UserRelation> BecomeFriendsAsync(ApplicationUser subscriber, ApplicationUser user);
}