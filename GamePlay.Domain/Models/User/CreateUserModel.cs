namespace GamePlay.Domain.Models.User;

public class CreateUserModel
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}