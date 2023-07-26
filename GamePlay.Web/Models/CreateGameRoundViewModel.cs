using GamePlay.Domain.Entities;
using GamePlay.Domain.Models.GameRound;
using GamePlay.Domain.Models.User;

namespace GamePlay.Web.Models;

public class CreateGameRoundViewModel
{
    public CreateGameRoundModel? GameRound { get; set; }
    public IEnumerable<string?>? PreviousPlaces { get; set; }
    public IEnumerable<string>? PreviousOpponents { get; set; }
    public IEnumerable<UserModel>? Users { get; set; }
}