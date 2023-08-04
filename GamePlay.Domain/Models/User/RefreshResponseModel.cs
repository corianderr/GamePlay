namespace GamePlay.Domain.Models.User;

public class RefreshResponseModel
{
    public IEnumerable<string>? Roles { get; set; }
    public string? AccessToken { get; set; }
    public string? Id { get; set; }
}