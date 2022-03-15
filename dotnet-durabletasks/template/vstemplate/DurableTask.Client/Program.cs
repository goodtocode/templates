using DurableTask.Activities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    internal class Program
    {
        static async Task Main()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            using ServiceProvider serviceProvider = services.BuildServiceProvider();
            Startup app = serviceProvider.GetService<Startup>();
            await app.Run(serviceProvider);
            while (true)
            {
                Thread.Sleep(1000);
            }
        }

        internal static void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder();
            var environment = Environment.GetEnvironmentVariable(EnvironmentVariableKeys.EnvironmentAspNetCore) ?? EnvironmentVariableDefaults.Environment;
            configuration
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json");
            
            var connection = Environment.GetEnvironmentVariable(EnvironmentVariableKeys.AppConfigurationConnection);
            if (connection?.Length > 0)
            {
                configuration.AddAzureAppConfiguration(options =>
                    options
                        .Connect(connection)
                        .ConfigureRefresh(refresh =>
                        {
                            refresh.Register(AppConfigurationKeys.SentinelKey, refreshAll: true)
                                    .SetCacheExpiration(new TimeSpan(0, 60, 0));
                        })
                        .Select(KeyFilter.Any, LabelFilter.Null)
                        .Select(KeyFilter.Any, environment));
            }
            var config = configuration.Build();
            services.AddScoped<IConfiguration>(_ => config);
            services.AddLogging(configure => configure.AddConsole())
                .AddSingleton(configuration)
                .AddTransient<Startup>();
        }
    }
}
