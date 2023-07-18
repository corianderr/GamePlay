﻿using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Duende.IdentityServer.EntityFramework.Options;
using GamePlay.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GamePlay.DAL.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Game>? Games { get; set; }
    public DbSet<UserRelation>? UserRelations { get; set; }
    public DbSet<GameRating>? GameRatings { get; set; }
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ConfigureEntityId<Game>(modelBuilder);
        ConfigureEntityId<UserRelation>(modelBuilder);
        ConfigureEntityId<GameRating>(modelBuilder);
    }
    
    private void ConfigureEntityId<T>(ModelBuilder modelBuilder) where T : BaseEntity
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
        foreach (var entry in ChangeTracker.Entries<BaseEntity>()){
            if (entry.State.Equals(EntityState.Added))
                entry.Entity.Id = Guid.NewGuid();
        }
        return await base.SaveChangesAsync(cancellationToken);
    }
}