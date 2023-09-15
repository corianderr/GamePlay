using GamePlay.Domain.Models.Player;

namespace GamePlay.Domain.Models.RoundPlayer;

public class RoundPlayerModel {
    public Guid Id { get; set; }
    public Guid GameRoundId { get; set; }
    public int Score { get; set; }
    public bool IsWinner { get; set; }
    public string? Role { get; set; }
    public Guid PlayerId { get; set; }
    public PlayerModel? Player { get; set; }
}