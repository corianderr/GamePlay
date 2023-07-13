using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Duende.IdentityServer.EntityFramework.Options;
using GamePlay.Models;

namespace GamePlay.Data;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
        : base(options, operationalStoreOptions)
    {
    }
    public DbSet<Game>? Games { get; set; }
    public DbSet<UserRelation>? UserRelations { get; set; }
    public DbSet<GameRating>? GameRatings { get; set; }
}