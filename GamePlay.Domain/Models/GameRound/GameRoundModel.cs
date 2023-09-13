using GamePlay.Domain.Entities;
using GamePlay.Domain.Models.Player;
using GamePlay.Domain.Models.User;

namespace GamePlay.Domain.Models.GameRound;

public class GameRoundModel {
    public Guid Id { get; set; }
    public Guid GameId { get; set; }
    public Entities.Game? Game { get; set; }
    public DateTime Date { get; set; }
    public string? Place { get; set; }
    public string? CreatorId { get; set; }
    public UserModel? Creator { get; set; }
    public List<RoundPlayerModel>? Players { get; set; }
}