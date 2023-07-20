using GamePlay.Domain.Enums;
using GamePlay.Domain.Models.User;

namespace GamePlay.Web.Models;

public class UserRelationViewModel
{
    public UserResponseModel? User { get; set; }
    public RelationOptions RelationOption { get; set; }
}