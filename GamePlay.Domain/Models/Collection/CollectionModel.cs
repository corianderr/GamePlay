using GamePlay.Domain.Entities;

namespace GamePlay.Domain.Models.Collection;

public class CollectionModel : CreateCollectionModel
{
    public Guid Id { get; set; }
    public ApplicationUser? User { get; set; }
    public List<Entities.Game>? Games { get; set; }
}