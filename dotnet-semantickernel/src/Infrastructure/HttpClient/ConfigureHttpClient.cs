using Microsoft.Extensions.DependencyInjection;
using Polly;
using SemanticKernelMicroservice.Infrastructure.HttpClient;

namespace SemanticKernelMicroservice.Core.Application;

public static class ConfigureSecuredHttpClient
{
    public static IServiceCollection AddHttpClientJitterService(this IServiceCollection services, string clientName, double firstDelaySeconds = 1.0, int retryCount = 5)
    {
        services.AddHttpClient(clientName)
            .AddTransientHttpErrorPolicy(builder =>
                builder.WaitAndRetryAsync(retryCount, retryAttempt =>
                    TimeSpan.FromSeconds(firstDelaySeconds * Math.Pow(2, retryAttempt))
                )
            )
            .AddHttpMessageHandler<TokenHandler>();

        services.AddSingleton<BearerToken>();
        services.AddTransient<TokenHandler>();

        return services;
    }
}