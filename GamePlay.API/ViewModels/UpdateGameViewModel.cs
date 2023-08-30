using GamePlay.Domain.Models.Game;

namespace GamePlay.API.ViewModels; 

public class UpdateGameViewModel {
    public Guid Id { get; set; }
    public UpdateGameModel? GameModel { get; set; }
    public string? GameModelJson { get; set; }
    public IFormFile? GameImage { get; set; }
}