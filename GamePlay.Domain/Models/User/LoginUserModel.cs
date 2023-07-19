namespace GamePlay.Domain.Models.User;

public class LoginUserModel
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public bool RememberMe { get; set; }
}