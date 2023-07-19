namespace GamePlay.Domain.Models.User;

public class UserResponseModel : BaseResponseModel
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? PhotoPath { get; set; }
    public int FriendsCount { get; set; }
    public int FollowersCount { get; set; }
}