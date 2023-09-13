using AutoMapper.Extensions.ExpressionMapping;
using GamePlay.DAL.Data;
using GamePlay.DAL.MappingProfiles;
using GamePlay.DAL.Repositories;
using GamePlay.Domain.Contracts.Repositories;
using GamePlay.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GamePlay.DAL;

public static class DataAccessDependencyInjection {
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration) {
        services.AddRepositories();
        services.RegisterAutoMapper();
        services.AddDatabase(configuration);
        services.AddIdentity();
        return services;
    }

    private static void AddRepositories(this IServiceCollection services) {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<IGameRatingRepository, GameRatingRepository>();
        services.AddScoped<IUserRelationRepository, UserRelationRepository>();
        services.AddScoped<ICollectionRepository, CollectionRepository>();
        services.AddScoped<IGameRoundRepository, GameRoundRepository>();
        services.AddScoped<IRoundPlayerRepository, RoundPlayerRepository>();
    }

    private static void RegisterAutoMapper(this IServiceCollection services) {
        services.AddAutoMapper(cfg => { cfg.AddExpressionMapping(); }, typeof(IMappingProfilesMarker));
    }

    private static void AddDatabase(this IServiceCollection services, IConfiguration configuration) {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
    }

    private static void AddIdentity(this IServiceCollection services) {
        services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.Configure<IdentityOptions>(options => {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 4;
            options.Password.RequiredUniqueChars = 1;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;
        });
    }
}