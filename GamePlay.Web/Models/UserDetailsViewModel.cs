using GamePlay.Domain.Enums;
using GamePlay.Domain.Models.Game;
using GamePlay.Domain.Models.User;

namespace GamePlay.Web.Models;

public class UserDetailsViewModel
{
    public UserModel? User { get; set; }
    public RelationOptions RelationOption { get; set; }
    public List<GameModel> Games { get; set; }
}