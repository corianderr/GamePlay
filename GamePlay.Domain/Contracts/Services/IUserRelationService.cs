using GamePlay.Domain.Models;
using GamePlay.Domain.Models.User;

namespace GamePlay.Domain.Contracts.Services;

public interface IUserRelationService {
    Task<UserRelationModel?> GetByUsersIdAsync(string subscriberId, string userId,
        CancellationToken cancellationToken = default);

    Task<BaseModel> SubscribeAsync(string subscriberId, string userId);
    Task<UserRelationModel> BecomeFriendsAsync(string subscriberId, string userId);

    Task<IEnumerable<UserRelationModel>> GetAllAsync(string userId,
        bool? isFriend = null);
    Task<int> GetNotificationsCount(string userId);
}