namespace GamePlay.Domain.Entities;

public class Collection {
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? UserId { get; set; }
    public bool IsDefault { get; set; }
    public ApplicationUser? User { get; set; }
    public List<Game> Games { get; set; }

    public Collection() {
        Games = new List<Game>();
    }
}