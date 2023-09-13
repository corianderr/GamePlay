using System.ComponentModel.DataAnnotations;
using GamePlay.Domain.Models.Game;
using GamePlay.Domain.Models.Player;
using GamePlay.Domain.Models.User;

namespace GamePlay.Domain.Models.GameRound;

public class CreateGameRoundModel {
    [Required]
    public Guid GameId { get; set; }
    public GameModel? Game { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required(ErrorMessage = "Please enter place name using less than 50 characters.")]
    [StringLength(50)]
    public string? Place { get; set; }
    public string? CreatorId { get; set; }
    public UserModel? Creator { get; set; }
    public List<CreateRoundPlayerModel>? Players { get; set; }
}