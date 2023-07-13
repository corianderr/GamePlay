using Microsoft.AspNetCore.Identity;

namespace GamePlay.Models;

public class ApplicationUser : IdentityUser
{
    public string PhotoPath { get; set; }
}