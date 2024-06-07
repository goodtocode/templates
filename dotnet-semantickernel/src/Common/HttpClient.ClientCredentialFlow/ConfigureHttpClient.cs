using Microsoft.Extensions.DependencyInjection;

namespace GoodToCode.HttpClient.ClientCredentialFlow;

public static class ConfigureSecuredHttpClient
{
    public static IServiceCollection AddHttpClientJitterService(this IServiceCollection services, string clientName, int maxRetry = 5)
    {
        services.AddHttpClient(clientName)
            .AddHttpMessageHandler<TokenHandler>()
            .AddStandardResilienceHandler(options =>
            {
                options.Retry.UseJitter = true;
                options.Retry.MaxRetryAttempts = maxRetry;
            });

        services.AddSingleton<BearerToken>();
        services.AddTransient<TokenHandler>();

        return services;
    }
}