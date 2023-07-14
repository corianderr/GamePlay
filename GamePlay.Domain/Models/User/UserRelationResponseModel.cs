using GamePlay.Domain.Entities;

namespace GamePlay.Domain.Models.User;

public class UserRelationResponseModel : CreateUserRelationModel
{
    public ApplicationUser? Subscriber { get; set; }
}