using GamePlay.Domain.Entities;

namespace GamePlay.Domain.Models.Collection;

public class CollectionModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }
    public List<Entities.Game>? Games { get; set; }
}