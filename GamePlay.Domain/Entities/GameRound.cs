namespace GamePlay.Domain.Entities;

public class GameRound
{
    public Guid Id { get; set; }
    public Guid GameId { get; set; }
    public Game? Game { get; set; }
    public DateTime Date { get; set; }
    public string? Place { get; set; }
    public List<Player> Players { get; set; }

    public GameRound()
    {
        Players = new List<Player>();
    }
}