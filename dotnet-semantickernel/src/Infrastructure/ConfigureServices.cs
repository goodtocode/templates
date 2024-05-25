using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using SemanticKernelMicroservice.Core.Application.Common.Interfaces;
using SemanticKernelMicroservice.Infrastructure.Persistence;

namespace SemanticKernelMicroservice.Infrastructure;

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

    public static IServiceCollection AddSemanticKernelServices(this IServiceCollection services,
    IConfiguration configuration)
    {
        services.AddKernel();
        services.AddOpenAIChatCompletion(configuration.GetValue<string>("AI:OpenAI:Model") ?? "gtp-3.5-turbo",
            configuration.GetValue<string>("AI:OpenAI:ApiKey") ?? string.Empty);

        return services;
    }
}