namespace GamePlay.Domain.Models.User;

public class LoginResponseModel
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Token { get; set; }
}