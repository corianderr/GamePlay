namespace GamePlay.Domain.Models.Game;

public class GameModel : UpdateGameModel
{
    public Guid Id { get; set; }
    public double AverageRating { get; set; }
}