using GoodToCode.Shared.Blob.Excel;
using GoodToCode.Shared.Persistence.StorageTables;
using DurableTask.Activities;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    public class Program
    {
        public static async Task Main()
        {
            EnvironmentVariables.Validate();
            var environment = Environment.GetEnvironmentVariable(EnvironmentVariableKeys.EnvironmentAspNetCore) ?? EnvironmentVariableDefaults.Environment;

            var host = new HostBuilder()
                .ConfigureAppConfiguration(c =>
                {
                    c.AddEnvironmentVariables();                    
                    c.AddJsonFile("appsettings.json").AddJsonFile($"appsettings.{environment}.json");
                    var connection = Environment.GetEnvironmentVariable(EnvironmentVariableKeys.AppConfigurationConnection);
                    if (connection?.Length > 0)
                    {
                        c.AddAzureAppConfiguration(options =>
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
                })
                .ConfigureServices(s =>
                {
                    s.AddTransient<IExcelService, ExcelService>();
                    s.AddTransient<IStorageTablesServiceConfiguration, StorageTablesServiceConfiguration>();
                    s.AddTransient(typeof(FanOutOrchestration));
                    s.AddTransient(typeof(GetDataActivity));
                    s.AddTransient(typeof(ProcessDataActivity));
                })
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureOpenApi()
                .Build();

            await host.RunAsync();
        }
    }
}