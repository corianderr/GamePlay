namespace GamePlay.Domain.Entities;

public class UserRelation : BaseEntity
{
    public Guid SubscriberId { get; set; }
    public ApplicationUser? Subscriber { get; set; }
    public Guid UserId { get; set; }
    public ApplicationUser? User { get; set; }
    public bool IsFriend { get; set; }
}