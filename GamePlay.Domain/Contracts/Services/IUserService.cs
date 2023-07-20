using System.Linq.Expressions;
using GamePlay.Domain.Entities;
using GamePlay.Domain.Models;
using GamePlay.Domain.Models.User;

namespace GamePlay.Domain.Contracts.Services;

public interface IUserService
{
    Task<BaseModel> RegisterAsync(CreateUserModel createUserModel);
    Task<LoginUserModel> LoginAsync(LoginUserModel loginUserModel);
    Task AddGameToUserAsync(Guid gameId, string userId);
    Task<BaseModel> SubscribeAsync(string subscriberId, string userId);
    Task<UserRelationModel> BecomeFriendsAsync(string subscriberId, string userId);

    Task<IEnumerable<UserRelationModel>> GetAllRelationsAsync(string userId,
        bool? isFriend = null);
    Task<IEnumerable<UserModel>> GetAllAsync(Expression<Func<UserModel, bool>>? predicate = null);

    Task<UserRelationModel?> GetRelationByUsersIdAsync(string subscriberId, string userId,
        CancellationToken cancellationToken = default);

    Task<UserModel> GetFirstAsync(string userId, CancellationToken cancellationToken = default);

    Task<UserModel> UpdateAsync(string id, UserModel updateUserModel,
        CancellationToken cancellationToken = default);
}