namespace GamePlay.Domain.Models.GameRound;

public class CreateGameRoundModel
{
    public Guid GameId { get; set; }
    public DateTime Date { get; set; }
    public string? Place { get; set; }
}