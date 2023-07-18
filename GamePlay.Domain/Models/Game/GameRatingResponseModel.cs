namespace GamePlay.Domain.Models.Game;

public class GameRatingResponseModel : BaseResponseModel
{
    public string? UserId { get; set; }
    public Guid? GameId { get; set; }
    public int Rating { get; set; }
}