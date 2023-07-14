using GamePlay.Domain.Entities;

namespace GamePlay.Domain.Models.User;

public class UserRelationResponseModel : UserRelationCreateModel
{
    public ApplicationUser? Subscriber { get; set; }
}