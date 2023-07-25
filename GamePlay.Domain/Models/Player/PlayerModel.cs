namespace GamePlay.Domain.Models.Player;

public class PlayerModel
{
    public Guid Id { get; set; }
    public Guid GameRoundId { get; set; }
    public string Name { get; set; }
    public int Score { get; set; }
    public bool IsWinner { get; set; }
    public string? Role { get; set; }
    public bool IsRegistered { get; set; }
    public string? UserId { get; set; }
}