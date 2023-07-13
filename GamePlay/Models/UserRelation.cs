namespace GamePlay.Models;

public class UserRelation : BaseEntity
{
    public string? SubscriberId { get; set; }
    public ApplicationUser? Subscriber { get; set; }
    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }
    public bool IsFriend { get; set; }
}