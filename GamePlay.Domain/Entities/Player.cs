using System.ComponentModel.DataAnnotations.Schema;

namespace GamePlay.Domain.Entities;

public class Player
{
    public Guid Id { get; set; }
    [Column("GameRoundId")]
    public Guid GameRoundId { get; set; }
    [Column("GameRound")]
    public GameRound? GameRound { get; set; }
    public string Name { get; set; }
    public int Score { get; set; }
    public bool IsWinner { get; set; }
    public string? Role { get; set; }
    public bool IsRegistered { get; set; }
    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }
}