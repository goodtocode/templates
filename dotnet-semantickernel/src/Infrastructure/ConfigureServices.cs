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
        // Chat conversation, such as Q&A
        services.AddOpenAIChatCompletion(configuration.GetValue<string>("AI:OpenAI:Model") ?? "gtp-3.5-turbo",
            configuration.GetValue<string>("AI:OpenAI:ApiKey") ?? string.Empty)
        // Completing words or sentences, code completion
        .AddOpenAITextGeneration(configuration.GetValue<string>("AI:OpenAI:Model") ?? "gpt-3.5-turbo",
            configuration.GetValue<string>("AI:OpenAI:APIKey") ?? string.Empty);

#pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        services.AddOpenAIAudioToText(configuration.GetValue<string>("AI:OpenAI:Model") ?? "gpt-3.5-turbo",
                    configuration.GetValue<string>("AI:OpenAI:APIKey") ?? string.Empty)
        .AddOpenAITextToAudio(configuration.GetValue<string>("AI:OpenAI:Model") ?? "gpt-3.5-turbo",
            configuration.GetValue<string>("AI:OpenAI:APIKey") ?? string.Empty);
#pragma warning restore SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

#pragma warning disable SKEXP0010 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        // Embedding text into a vector for storage in CosmosDb or Qdrant
        services.AddOpenAITextEmbeddingGeneration(configuration.GetValue<string>("AI:OpenAI:Model") ?? "gpt-3.5-turbo",
            configuration.GetValue<string>("AI:OpenAI:APIKey") ?? string.Empty)
        .AddOpenAIFiles(configuration.GetValue<string>("AI:OpenAI:Model") ?? "gpt-3.5-turbo",
            configuration.GetValue<string>("AI:OpenAI:APIKey") ?? string.Empty)
        .AddOpenAITextToImage(configuration.GetValue<string>("AI:OpenAI:Model") ?? "gpt-3.5-turbo",
            configuration.GetValue<string>("AI:OpenAI:APIKey") ?? string.Empty);
#pragma warning restore SKEXP0010 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

        return services;
    }
}