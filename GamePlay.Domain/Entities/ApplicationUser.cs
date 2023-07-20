using Microsoft.AspNetCore.Identity;

namespace GamePlay.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public ApplicationUser()
    {
        Games = new List<Game>();
    }

    public string? PhotoPath { get; set; }
    public int FriendsCount { get; set; }
    public int FollowersCount { get; set; }
    public List<Game> Games { get; set; }
}