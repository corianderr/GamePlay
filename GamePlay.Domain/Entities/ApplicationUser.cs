using Microsoft.AspNetCore.Identity;

namespace GamePlay.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string? PhotoPath { get; set; }
    public List<Game> Games { get; set; }

    public ApplicationUser() : base()
    {
        Games = new List<Game>();
    }
}