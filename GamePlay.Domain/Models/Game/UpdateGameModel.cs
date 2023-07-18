using System.ComponentModel.DataAnnotations;
using GamePlay.Domain.CustomAttributes;

namespace GamePlay.Domain.Models.Game;

public class UpdateGameModel
{
    [Required(ErrorMessage = "Please enter game name using less than 50 characters.")]
    [StringLength(50)]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Please enter game name in russian using less than 50 characters.")]
    [StringLength(50)]
    public string? NameRu { get; set; }
    [Required(ErrorMessage = "Please enter game name in english using less than 50 characters.")]
    [StringLength(50)]
    public string? NameEn { get; set; }
    public string? PhotoPath { get; set; }
    [Range(2, 100, ErrorMessage = "Min players value must be between 2 and 100")]
    [Required(ErrorMessage = "Please enter minimum number of players.")]
    public int MinPlayers { get; set; }
    [Range(2, 100, ErrorMessage = "Max players value must be between 2 and 100")]
    [Required(ErrorMessage = "Please enter maximum number of players.")]
    [NotLess(nameof(MinPlayers), ErrorMessage = "Maximum number must be greater than minimum number.")]
    public int MaxPlayers { get; set; }
    [Range(0, 100, ErrorMessage = "Min age value must be between 0 and 100")]
    [Required(ErrorMessage = "Please enter minimum age.")]
    public int MinAge { get; set; }
    [Range(2, 100, ErrorMessage = "Min play time value must be between 2 and 100")]
    [Required(ErrorMessage = "Please enter minimum play time.")]
    public int MinPlayTime { get; set; }
    [Range(2, 100, ErrorMessage = "Max play time value must be between 2 and 100")]
    [Required(ErrorMessage = "Please enter maximum play time.")]
    [NotLess(nameof(MinPlayTime), ErrorMessage = "Maximum number must be greater than minimum number.")]
    public int MaxPlayTime { get; set; }
    [YearRange(1900, ErrorMessage = "Year of release value must be greater than 1900 or equal or less than current year.")]
    [Required(ErrorMessage = "Please enter year of release.")]
    public int YearOfRelease { get; set; }
}