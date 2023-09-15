namespace GamePlay.Domain.Models.RoundPlayer;

public class CreateRoundPlayerModel {
    public Guid? GameRoundId { get; set; }
    public int Score { get; set; }
    public bool IsWinner { get; set; }
    public string? Role { get; set; }
    public Guid PlayerId { get; set; }
}