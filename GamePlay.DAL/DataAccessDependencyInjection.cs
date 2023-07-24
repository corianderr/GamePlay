using AutoMapper.Extensions.ExpressionMapping;
using GamePlay.DAL.MappingProfiles;
using GamePlay.DAL.Repositories;
using GamePlay.Domain.Contracts.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GamePlay.DAL;

public static class DataAccessDependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepositories();
        services.RegisterAutoMapper();
        return services;
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<IGameRatingRepository, GameRatingRepository>();
        services.AddScoped<IUserRelationRepository, UserRelationRepository>();
        services.AddScoped<ICollectionRepository, CollectionRepository>();
    }

    private static void RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => { cfg.AddExpressionMapping(); }, typeof(IMappingProfilesMarker));
    }
}