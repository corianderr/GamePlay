namespace GamePlay.Domain.Models.Player;

public class PlayerModel {
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public bool IsRegistered { get; set; }
    public string? UserId { get; set; }
}