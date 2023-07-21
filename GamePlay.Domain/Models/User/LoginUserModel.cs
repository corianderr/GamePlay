namespace GamePlay.Domain.Models.User;

public class LoginUserModel
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public bool RememberMe { get; set; }
}