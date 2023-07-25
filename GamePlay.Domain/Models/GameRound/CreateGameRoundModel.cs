using System.ComponentModel.DataAnnotations;

namespace GamePlay.Domain.Models.GameRound;

public class CreateGameRoundModel
{
    [Required]
    public Guid GameId { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required(ErrorMessage = "Please enter place name using less than 50 characters.")]
    [StringLength(50)]
    public string? Place { get; set; }

    public List<Entities.Player>? Players { get; set; }
}