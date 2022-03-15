using GoodToCode.Shared.Persistence.StorageTables;
using DurableTask.Activities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DurableTask.Worker
{
    internal class Program
    {
        private static string environment;
        private static IConfiguration config;
        public static async Task Main()
        {
            EnvironmentVariables.Validate();
            environment = Environment.GetEnvironmentVariable(EnvironmentVariableKeys.EnvironmentAzureFunctions)
                                    ?? Environment.GetEnvironmentVariable(EnvironmentVariableKeys.EnvironmentAspNetCore)
                                    ?? EnvironmentVariableDefaults.Environment;

            var host = new HostBuilder()
                .ConfigureAppConfiguration(c =>
                {
                    c.AddEnvironmentVariables();
                    c.AddAzureAppConfiguration(options =>
                        options
                            .Connect(Environment.GetEnvironmentVariable(EnvironmentVariableKeys.AppConfigurationConnection))
                            .ConfigureRefresh(refresh =>
                            {
                                refresh.Register(AppConfigurationKeys.SentinelKey, refreshAll: true)
                                       .SetCacheExpiration(new TimeSpan(24, 0, 0));
                            })
                            .Select(KeyFilter.Any, LabelFilter.Null)
                            .Select(KeyFilter.Any, environment)
                    );
                    config = c.Build();
                    c.AddInMemoryCollection(new Dictionary<string, string>() {
                            { "ConnectionStrings:AzureWebJobsStorage",  config[AppConfigurationKeys.StorageTablesConnectionString]}
                        }); // Conform to IHostBuilder.ConfigureWebJobs() convention
                })
                .ConfigureWebJobs(b =>
                {
                    b.AddAzureStorageCoreServices();
                    b.AddTimers();
                    b.AddAzureStorageBlobs();
                    b.AddAzureStorageQueues();
                })
                .UseEnvironment(environment)
                .ConfigureLogging((context, b) =>
                {
                    b.AddConsole();
                })
                .ConfigureServices(s =>
                {
                    s.AddLogging();
                    s.AddTransient<IStorageTablesServiceConfiguration, StorageTablesServiceConfiguration>();
                    s.AddTransient(typeof(FanOutOrchestration));
                    s.AddTransient(typeof(GetDataActivity));
                    s.AddTransient(typeof(ProcessDataActivity));
                })
                .Build();
            
            using(host)
                await host.RunAsync();
        }
    }
}