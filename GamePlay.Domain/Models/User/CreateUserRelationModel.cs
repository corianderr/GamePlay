namespace GamePlay.Domain.Models.User;

public class CreateUserRelationModel {
    public string? SubscriberId { get; set; }
    public string? UserId { get; set; }
    public bool IsFriend { get; set; }
}