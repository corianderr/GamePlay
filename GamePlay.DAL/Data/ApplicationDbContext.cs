using GamePlay.DAL.Extensions;
using GamePlay.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GamePlay.DAL.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Game>? Games { get; set; }
    public DbSet<UserRelation>? UserRelations { get; set; }
    public DbSet<GameRating>? GameRatings { get; set; }
    public DbSet<Collection>? Collections { get; set; }
    public DbSet<GameResult>? GameResults { get; set; }
    public DbSet<Player>? Players { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ConfigureEntityId<BaseEntity>(modelBuilder);
        ConfigureEntityId<Collection>(modelBuilder);
        ConfigureEntityId<Game>(modelBuilder);
        ConfigureEntityId<GameRating>(modelBuilder);
        ConfigureEntityId<GameResult>(modelBuilder);
        ConfigureEntityId<Player>(modelBuilder);
        ConfigureEntityId<UserRelation>(modelBuilder);
        
        modelBuilder.Seed();
    }

    private static void ConfigureEntityId<T>(ModelBuilder modelBuilder) where T : class
    {
        modelBuilder.Entity<T>(b =>
        {
            b.Property<Guid>("Id")
                .HasColumnType("uniqueidentifier")
                .ValueGeneratedOnAdd();

            b.HasKey("Id");
        });
    }

    public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            if (entry.State.Equals(EntityState.Added))
                entry.Entity.Id = Guid.NewGuid();
        return await base.SaveChangesAsync(cancellationToken);
    }
}