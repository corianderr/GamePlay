namespace GamePlay.Domain.Models.Game;

public class GameModel {
    public Guid Id { get; set; }
    public double AverageRating { get; set; }
    public string? Name { get; set; }
    public string? NameRu { get; set; }
    public string? NameEn { get; set; }
    public string? Description { get; set; }
    public string? PhotoPath { get; set; }
    public int MinPlayers { get; set; }
    public int MaxPlayers { get; set; }
    public int MinAge { get; set; }
    public int MinPlayTime { get; set; }
    public int MaxPlayTime { get; set; }
    public int YearOfRelease { get; set; }

}