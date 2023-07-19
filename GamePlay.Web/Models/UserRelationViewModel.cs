using GamePlay.Domain.Models.User;

namespace GamePlay.Web.Models;

public class UserRelationViewModel
{
    public UserResponseModel? User { get; set; }
    public bool? IsFriend { get; set; }
    public int FollowersCount { get; set; }
    public int FriendsCount { get; set; }
}