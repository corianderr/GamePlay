namespace GamePlay.Domain.Models.Player;

public class CreatePlayerModel {
    public string? Name { get; set; }
    public bool IsRegistered { get; set; }
    public string? UserId { get; set; }
}