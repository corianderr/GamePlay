using GamePlay.Domain.Enums;
using GamePlay.Domain.Models.Collection;
using GamePlay.Domain.Models.User;

namespace GamePlay.API.ViewModels;

public class UserDetailsViewModel
{
    public UserModel? User { get; set; }
    public RelationOptions RelationOption { get; set; }
    public IEnumerable<CollectionModel> Collections { get; set; }
    public bool IsCurrentUser { get; set; }
}