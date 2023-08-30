namespace GamePlay.API.ViewModels; 

public class UpdateUserViewModel {
    public string? Id { get; set; }
    public string? PreviousPhotoPath { get; set; }
    public IFormFile? Avatar { get; set; }
}