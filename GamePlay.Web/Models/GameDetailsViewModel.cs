using GamePlay.Domain.Models.Collection;
using GamePlay.Domain.Models.Game;

namespace GamePlay.Web.Models;

public class GameDetailsViewModel
{
    public GameModel? Game { get; set; }
    public GameRatingModel? Rating { get; set; }
    public IEnumerable<CollectionModel> AvailableCollections { get; set; }
}