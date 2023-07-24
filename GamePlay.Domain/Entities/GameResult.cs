namespace GamePlay.Domain.Entities;

public class GameResult
{
    public Guid Id { get; set; }
    public Guid GameId { get; set; }
    public Game? Game { get; set; }
    public DateTime Date { get; set; }
    public string? Place { get; set; }
}