namespace GamePlay.Domain.Entities;

public class UserRelation {
    public Guid Id { get; set; }
    public string? SubscriberId { get; set; }
    public ApplicationUser? Subscriber { get; set; }
    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }
    public bool IsFriend { get; set; }
}