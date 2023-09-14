using System.ComponentModel.DataAnnotations.Schema;

namespace GamePlay.Domain.Entities;

public class RoundPlayer {
    public Guid Id { get; set; }
    [Column("GameRoundId")]
    public Guid GameRoundId { get; set; }
    [Column("GameRound")]
    public GameRound? GameRound { get; set; }
    public int Score { get; set; }
    public bool IsWinner { get; set; }
    public string? Role { get; set; }
    public Guid PlayerId { get; set; }
    public Player? Player { get; set; }
}