namespace GamePlay.Domain.Entities;

public class Player
{
    public Guid Id { get; set; }
    public Guid GameResultId { get; set; }
    public GameResult? GameResult { get; set; }
    public int Score { get; set; }
    public bool IsWinner { get; set; }
    public string? Role { get; set; }
    public bool IsRegistered { get; set; }
    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }
}