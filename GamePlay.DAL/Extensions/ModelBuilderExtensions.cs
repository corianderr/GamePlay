using GamePlay.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GamePlay.DAL.Extensions;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        var adminId = Guid.NewGuid().ToString();
        var adminRoleId = Guid.NewGuid().ToString();
        
        modelBuilder.Entity<IdentityRole>().HasData(
            new List<IdentityRole>
            {
                new() { Id = adminRoleId, Name = "admin", NormalizedName = "admin" },
                new() { Id = Guid.NewGuid().ToString(), Name = "user", NormalizedName = "user" }
            });

        var hasher = new PasswordHasher<ApplicationUser>();
        modelBuilder.Entity<ApplicationUser>().HasData(
            new ApplicationUser()
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com",
                EmailConfirmed = false,
                PasswordHash = hasher.HashPassword(null, "Admin123#"),
                SecurityStamp = string.Empty,
                PhotoPath = "/avatars/default-user-avatar.jpg"
            }
        );
        
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = adminRoleId,
            UserId = adminId
        });
    }
}