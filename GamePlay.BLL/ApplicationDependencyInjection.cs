using GamePlay.BLL.Services;
using GamePlay.BLL.Services.Interfaces;
using GamePlay.DAL.MappingProfiles;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace GamePlay.BLL;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IWebHostEnvironment env)
    {
        services.AddServices(env);
        return services;
    }

    private static void AddServices(this IServiceCollection services, IWebHostEnvironment env)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IGameService, GameService>();
    }
}