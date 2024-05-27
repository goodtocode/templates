﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.AudioToText;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Embeddings;
using Microsoft.SemanticKernel.Memory;
using Microsoft.SemanticKernel.TextGeneration;
using Microsoft.SemanticKernel.TextToAudio;
using Microsoft.SemanticKernel.TextToImage;
using SemanticKernelMicroservice.Core.Application.Common.Interfaces;
using SemanticKernelMicroservice.Infrastructure.Common;
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
        // Add strongly-typed and validated options for downstream use via DI.
        services.AddOptions<OpenAI>()
        .Bind(configuration.GetSection(nameof(OpenAI)))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        services.AddKernel();

        // Chat Completion
        // Alternative: services.AddOpenAIChatCompletion(configuration.GetValue<string>("OpenAI:ChatModelId"), configuration.GetValue<string>("OpenAI:ApiKey"))
        services.AddSingleton<IChatCompletionService>(sp =>
        {
            OpenAI options = sp.GetRequiredService<IOptions<OpenAI>>().Value;
            return new OpenAIChatCompletionService(options.ChatModelId, options.ApiKey);
        })
        // Completing words or sentences, code completion
        // Alternative: services.AddOpenAITextGeneration(configuration.GetValue<string>("OpenAI:ChatModelId"), configuration.GetValue<string>("OpenAI:ApiKey"))
        .AddSingleton<ITextGenerationService>(sp =>
        {
            OpenAI options = sp.GetRequiredService<IOptions<OpenAI>>().Value;
            return new OpenAITextGenerationService(options.ChatModelId, options.ApiKey);
        });
#pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0010 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        // Translate audio to text
        // Alternative: services.AddOpenAIAudioToText(configuration.GetValue<string>("OpenAI:ChatModelId"), configuration.GetValue<string>("OpenAI:ApiKey"))
        services.AddSingleton<IAudioToTextService>(sp =>
        {
            OpenAI options = sp.GetRequiredService<IOptions<OpenAI>>().Value;
            return new OpenAIAudioToTextService(options.ChatModelId, options.ApiKey);
        })
        // Translate audio to text
        // Alternative: services.AddOpenAITextToAudio(configuration.GetValue<string>("OpenAI:ChatModelId"), configuration.GetValue<string>("OpenAI:ApiKey"))
        .AddSingleton<ITextToAudioService>(sp =>
        {
            OpenAI options = sp.GetRequiredService<IOptions<OpenAI>>().Value;
            return new OpenAITextToAudioService(options.ChatModelId, options.ApiKey);
        })
        // Embedding text into a vector for storage in CosmosDb or Qdrant
        // Alternative: services.AddOpenTextEmbeddingGeneration(configuration.GetValue<string>("OpenAI:ChatModelId"), configuration.GetValue<string>("OpenAI:ApiKey"))
        .AddSingleton<ITextEmbeddingGenerationService>(sp =>
        {
            OpenAI options = sp.GetRequiredService<IOptions<OpenAI>>().Value;
            return new OpenAITextEmbeddingGenerationService(options.ChatModelId, options.ApiKey);
        })
        // Translate text to image
        // Alternative: .AddOpenAITextToImage(configuration.GetValue<string>("OpenAI:ChatModelId")!, configuration.GetValue<string>("OpenAI:ApiKey"))
        .AddSingleton<ITextToImageService>(sp =>
        {
            OpenAI options = sp.GetRequiredService<IOptions<OpenAI>>().Value;
            return new OpenAITextToImageService(options.ChatModelId, options.ApiKey);
        })
        // File services
        //.AddOpenAIFiles(configuration.GetValue<string>("OpenAI:ChatModelId")!, configuration.GetValue<string>("OpenAI:ApiKey"))
        .AddSingleton<OpenAIFileService>(sp =>
        {
            OpenAI options = sp.GetRequiredService<IOptions<OpenAI>>().Value;
            return new OpenAIFileService(options.ChatModelId, options.ApiKey);
        });
#pragma warning restore SKEXP0001
#pragma warning restore SKEXP0010

        // ToDo: Implement MemoryBuilder.WithMemoryStore(VolatileMemoryStore or SQLMemoryStore)

        return services;
    }
}