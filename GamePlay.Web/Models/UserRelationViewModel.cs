using GamePlay.Domain.Models.User;
using GamePlay.Web.Enums;

namespace GamePlay.Web.Models;

public class UserRelationViewModel
{
    public UserResponseModel? User { get; set; }
    public RelationOptions RelationOption { get; set; }
}