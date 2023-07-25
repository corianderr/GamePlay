using GamePlay.Domain.Entities;
using GamePlay.Domain.Models.GameRound;

namespace GamePlay.Web.Models;

public class CreateGameRoundViewModel
{
    public CreateGameRoundModel? GameRound { get; set; }
    public IEnumerable<string>? PreviousPlaces { get; set; }
    public IEnumerable<Player>? PreviousOpponents { get; set; }
}