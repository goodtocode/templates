using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using dotnet_semantickernel.Core.Application.Common.Interfaces;
using dotnet_semantickernel.Infrastructure.Persistence;

namespace dotnet_semantickernel.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<dotnet_semantickernelContext>(options =>
                options.UseInMemoryDatabase("DefaultConnection").UseLazyLoadingProxies());
        }
        else
        {
            services.AddDbContext<dotnet_semantickernelContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                        builder => builder.MigrationsAssembly(typeof(dotnet_semantickernelContext).Assembly.FullName))
                    .UseLazyLoadingProxies());
        }

        services.AddScoped<Idotnet_semantickernelContext, dotnet_semantickernelContext>();

        services.AddScoped<dotnet_semantickernelDbContextInitializer>();

        return services;
    }
}