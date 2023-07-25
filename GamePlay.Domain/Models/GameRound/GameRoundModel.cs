using GamePlay.Domain.Entities;

namespace GamePlay.Domain.Models.GameRound;

public class GameRoundModel
{
    public Guid Id { get; set; }
    public Guid GameId { get; set; }
    public Entities.Game? Game { get; set; }
    public DateTime Date { get; set; }
    public string? Place { get; set; }
    public List<Entities.Player>? Players { get; set; }
}