using System.Linq.Expressions;
using GamePlay.Domain.Entities;
using GamePlay.Domain.Models;
using GamePlay.Domain.Models.User;

namespace GamePlay.BLL.Services.Interfaces;

public interface IUserService
{
    Task<BaseResponseModel> RegisterAsync(CreateUserModel createUserModel);
    Task<LoginUserModel> LoginAsync(LoginUserModel loginUserModel);
    Task AddGameToUserAsync(Guid gameId, string userId);
    Task<BaseResponseModel> SubscribeAsync(string subscriberId, string userId);
    Task<UserRelationResponseModel> BecomeFriendsAsync(string subscriberId, string userId);

    Task<IEnumerable<UserRelationResponseModel>> GetAllRelationsAsync(string userId,
        bool? isFriend = null);
    Task<IEnumerable<UserResponseModel>> GetAllAsync(Expression<Func<UserResponseModel, bool>>? predicate = null);

    Task<UserRelationResponseModel> GetRelationByUserIdAsync(string userId,
        CancellationToken cancellationToken = default);
}