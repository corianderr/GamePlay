namespace GamePlay.Models;

public class UserRelation : BaseEntity
{
    public string SubscriberId { get; set; }
    public string UserId { get; set; }
    public bool IsFriend { get; set; }
}