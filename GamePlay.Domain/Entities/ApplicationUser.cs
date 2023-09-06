using Microsoft.AspNetCore.Identity;

namespace GamePlay.Domain.Entities;

public class ApplicationUser : IdentityUser {
    public string? PhotoPath { get; set; }
    public int FriendsCount { get; set; }
    public int FollowersCount { get; set; }
    public List<Collection> Collections { get; set; }

    public ApplicationUser() {
        Collections = new List<Collection>();
    }
}