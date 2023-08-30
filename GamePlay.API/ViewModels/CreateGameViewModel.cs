using GamePlay.Domain.Models.Game;

namespace GamePlay.API.ViewModels; 

public class CreateGameViewModel {
    public CreateGameModel? GameModel { get; set; }
    public string? GameModelJson { get; set; }
    public IFormFile? GameImage { get; set; }
}