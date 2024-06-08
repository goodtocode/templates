﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SemanticKernelMicroservice.Core.Application.Abstractions;
using SemanticKernelMicroservice.Infrastructure.SqlServer.Persistence;

namespace SemanticKernelMicroservice.Infrastructure.SqlServer;

public static class ConfigureServices
{
    public static IServiceCollection AddDbContextServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ChatCompletionContext>(options =>
                options.UseInMemoryDatabase("DefaultConnection").UseLazyLoadingProxies());
        }
        else
        {
            services.AddDbContext<ChatCompletionContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                        builder => builder.MigrationsAssembly(typeof(ChatCompletionContext).Assembly.FullName))
                    .UseLazyLoadingProxies());
        }

        services.AddScoped<IChatCompletionContext, ChatCompletionContext>();
        services.AddScoped<ChatCompletionContextInitializer>();

        return services;
    }
}