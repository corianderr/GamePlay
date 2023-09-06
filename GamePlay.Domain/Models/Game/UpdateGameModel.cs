using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GamePlay.Domain.CustomAttributes;

namespace GamePlay.Domain.Models.Game;

public class UpdateGameModel {
    [Required(ErrorMessage = "Please enter game name using less than 50 characters.")]
    [StringLength(50)]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Please enter game name in russian using less than 50 characters.")]
    [StringLength(50)]
    [DisplayName("Name in russian")]
    public string? NameRu { get; set; }

    [Required(ErrorMessage = "Please enter game name in english using less than 50 characters.")]
    [StringLength(50)]
    [DisplayName("Name in english")]
    public string? NameEn { get; set; }

    [Required(ErrorMessage = "Please enter game description.")]
    public string? Description { get; set; }

    public string? PhotoPath { get; set; }

    [Range(1, 100, ErrorMessage = "Min players value must be between 1 and 100")]
    [Required(ErrorMessage = "Please enter minimum number of players.")]
    [DisplayName("Minimum number of players")]
    public int MinPlayers { get; set; }

    [Range(1, 100, ErrorMessage = "Max players value must be between 1 and 100")]
    [Required(ErrorMessage = "Please enter maximum number of players.")]
    [NotLess(nameof(MinPlayers), ErrorMessage = "Maximum number must be greater than minimum number.")]
    [DisplayName("Maximum number of players")]
    public int MaxPlayers { get; set; }

    [Range(0, 100, ErrorMessage = "Min age value must be between 0 and 100")]
    [Required(ErrorMessage = "Please enter minimum age.")]
    [DisplayName("Minimum age")]
    public int MinAge { get; set; }

    [Range(1, 90000, ErrorMessage = "Min play time value must be between 1 and 90000 minutes." +
                                    "(No, you can not play more than 1500 hours straight, go and have a rest, see your family and world around)")]
    [Required(ErrorMessage = "Please enter minimum play time in minutes.")]
    [DisplayName("Minimum play time in minutes")]
    public int MinPlayTime { get; set; }

    [Range(1, 90000, ErrorMessage = "Max play time value must be between 1 and 90000 minutes. " +
                                    "(No, you can not play more than 1500 hours straight, go and have a rest, see your family and world around)")]
    [Required(ErrorMessage = "Please enter maximum play time in minutes.")]
    [NotLess(nameof(MinPlayTime), ErrorMessage = "Maximum number must be greater than minimum number.")]
    [DisplayName("Maximum play time in minutes")]
    public int MaxPlayTime { get; set; }

    [YearRange(1800,
        ErrorMessage =
            "You definitely messed something up. Games in such a distant past have not yet been released. Enter a year between 1800 and the current year.")]
    [Required(ErrorMessage = "Please enter year of release.")]
    [DisplayName("Year of Release")]
    public int YearOfRelease { get; set; }
}