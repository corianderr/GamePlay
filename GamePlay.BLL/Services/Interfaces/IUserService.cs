using GamePlay.Domain.Entities;
using GamePlay.Domain.Models;
using GamePlay.Domain.Models.User;

namespace GamePlay.BLL.Services.Interfaces;

public interface IUserService
{
    Task<BaseResponseModel> RegisterAsync(CreateUserModel createUserModel);
    Task<LoginUserModel> LoginAsync(LoginUserModel loginUserModel);
    Task AddGameToUserAsync(Guid gameId, Guid userId);
    Task<UserRelation> SubscribeAsync(Guid subscriberId, Guid userId);
    Task<UserRelation> BecomeFriendsAsync(Guid firstUserId, Guid secondUserId);
    Task<IEnumerable<UserRelation>> GetAllRelationsAsync(Guid userId, bool isFriend);
}