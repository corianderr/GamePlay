using GamePlay.Domain.Entities;

namespace GamePlay.Domain.Models.User;

public class UserRelationModel : BaseModel
{
    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }
    public bool IsFriend { get; set; }
    public string? SubscriberId { get; set; }
    public ApplicationUser? Subscriber { get; set; }
}