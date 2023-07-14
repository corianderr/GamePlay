namespace GamePlay.Domain.Entities;

public class GameRating : BaseEntity
{
    public Guid UserId { get; set; }
    public ApplicationUser? User { get; set; }
    public Guid GameId { get; set; }
    public Game? Game { get; set; }
    public int Rating { get; set; }
}