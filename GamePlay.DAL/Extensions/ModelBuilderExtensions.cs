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
        const string adminId = "964d01a4-af91-422a-9d7a-e88b02398b00";
        const string adminRoleId = "b6a28f69-2c96-42fa-9261-91d0815a900e";
        const string userRoleId = "064f3a21-31a4-4bee-861e-af3acba38b5b";
        
        modelBuilder.Entity<IdentityRole>().HasData(
            new List<IdentityRole>
            {
                new() { Id = adminRoleId, Name = "admin", NormalizedName = "admin", ConcurrencyStamp = "677ab182-1188-4ce7-bc6d-dfed51865740"},
                new() { Id = userRoleId, Name = "user", NormalizedName = "user", ConcurrencyStamp = "4057cd4c-f12d-42f4-a9bb-da60da1b4b26"}
            });

        var hasher = new PasswordHasher<ApplicationUser>();
        var user = new ApplicationUser()
        {
            Id = adminId,
            UserName = "admin",
            NormalizedUserName = "admin",
            Email = "admin@gmail.com",
            NormalizedEmail = "admin@gmail.com",
            EmailConfirmed = false,
            SecurityStamp = string.Empty,
            PhotoPath = "/avatars/default-user-avatar.jpg",
            ConcurrencyStamp = "c0ce3486-19bd-43d7-974c-973db66b3710",
            PasswordHash = "AQAAAAEAACcQAAAAEAGuVdh7FUcxb+87xaMRVQR2ZtfZnFFct0B1o6UocOCvxM7WEWEByAzEXbB3yQZzHg==" //Admin123#
        };
        modelBuilder.Entity<ApplicationUser>().HasData(user);
        
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = adminRoleId,
            UserId = adminId
        });
    }
}