namespace GamePlay.Domain.Models.User;

public class UserRelationCreateModel
{
    public string? SubscriberId { get; set; }
    public string? UserId { get; set; }
    public bool IsFriend { get; set; }
}