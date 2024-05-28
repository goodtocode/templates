using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherForecasts.Core.Application.Common.Interfaces;
using WeatherForecasts.Infrastructure.Persistence;

namespace WeatherForecasts.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddDbContextServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<WeatherForecastsContext>(options =>
                options.UseInMemoryDatabase("DefaultConnection").UseLazyLoadingProxies());
        }
        else
        {
            services.AddDbContext<WeatherForecastsContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                        builder => builder.MigrationsAssembly(typeof(WeatherForecastsContext).Assembly.FullName))
                    .UseLazyLoadingProxies());
        }

        services.AddScoped<IWeatherForecastsContext, WeatherForecastsContext>();

        services.AddScoped<WeatherForecastsDbContextInitializer>();

        return services;
    }
}