namespace GamePlay.Models;

public class UsersGame : BaseEntity
{
    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }
    public string? GameId { get; set; }
    public Game? Game { get; set; }
    public int Rating { get; set; }
}