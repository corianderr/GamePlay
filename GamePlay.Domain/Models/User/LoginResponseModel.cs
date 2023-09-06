using System.Text.Json.Serialization;

namespace GamePlay.Domain.Models.User;

public class LoginResponseModel {
    public string? Username { get; set; }
    public string? Id { get; set; }
    public string? Email { get; set; }
    public string? AccessToken { get; set; }
    [JsonIgnore]
    public string? RefreshToken { get; set; }
    [JsonIgnore]
    public DateTime RefreshTokenExpiryTime { get; set; }
    public IEnumerable<string>? Roles { get; set; }
}