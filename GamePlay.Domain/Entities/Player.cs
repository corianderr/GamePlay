namespace GamePlay.Domain.Entities; 

public class Player {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? UserId { get; set; }
    public bool IsRegistered { get; set; }
    public ApplicationUser? User { get; set; }
}