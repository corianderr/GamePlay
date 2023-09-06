using GamePlay.Domain.Entities;
using GamePlay.Domain.Models.Player;

namespace GamePlay.Domain.Models.GameRound;

public class GameRoundModel {
    public Guid Id { get; set; }
    public Guid GameId { get; set; }
    public Entities.Game? Game { get; set; }
    public DateTime Date { get; set; }
    public string? Place { get; set; }
    public List<PlayerModel>? Players { get; set; }
}