using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SemanticKernelMicroservice.Core.Application.Common.Interfaces;
using SemanticKernelMicroservice.Infrastructure.SqlServer.Persistence;

namespace SemanticKernelMicroservice.Infrastructure.SqlServer;

public static class ConfigureServices
{
    public static IServiceCollection AddDbContextServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<SemanticKernelMicroserviceContext>(options =>
                options.UseInMemoryDatabase("DefaultConnection").UseLazyLoadingProxies());
        }
        else
        {
            services.AddDbContext<SemanticKernelMicroserviceContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                        builder => builder.MigrationsAssembly(typeof(SemanticKernelMicroserviceContext).Assembly.FullName))
                    .UseLazyLoadingProxies());
        }

        services.AddScoped<ISemanticKernelMicroserviceContext, SemanticKernelMicroserviceContext>();
        services.AddScoped<SemanticKernelMicroserviceDbContextInitializer>();

        return services;
    }
}