namespace GamePlay.Domain.Models.User;

public class LoginResponseModel
{
    public string? Username { get; set; }
    public string? Id { get; set; }
    public string? Email { get; set; }
    public string? Token { get; set; }
    public IEnumerable<string>? Roles { get; set; }
}