using GamePlay.Domain.Entities;
using GamePlay.Domain.Models;
using GamePlay.Domain.Models.User;

namespace GamePlay.BLL.Services.Interfaces;

public interface IUserService
{
    Task<BaseResponseModel> RegisterAsync(CreateUserModel createUserModel);
    Task<LoginUserModel> LoginAsync(LoginUserModel loginUserModel);
    Task AddGameToUserAsync(Guid gameId, Guid userId);
    Task<UserRelationResponseModel> SubscribeAsync(Guid subscriberId, Guid userId);
    Task<UserRelationResponseModel> BecomeFriendsAsync(Guid subscriberId, Guid userId);
    Task<IEnumerable<UserRelationResponseModel>> GetAllRelationsAsync(Guid userId, bool isFriend);
}