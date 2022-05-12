using DurableTask.Core;
using DurableTask.ServiceBus;
using DurableTask.ServiceBus.Settings;
using DurableTask.ServiceBus.Tracking;
using DurableTask.Activities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    internal class Startup
    {
        internal async Task Run(ServiceProvider serviceProvider)
        {
            var config = serviceProvider.GetService<IConfiguration>();
            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();
            try
            {
                logger.LogDebug("Starting application");
                IOrchestrationServiceInstanceStore store = new AzureTableInstanceStore(config[AppConfigurationKeys.HubName], config[AppConfigurationKeys.StorageTablesConnectionString]);
                var settings = new ServiceBusOrchestrationServiceSettings();
                var service = new ServiceBusOrchestrationService(config[AppConfigurationKeys.ServiceBusConnectionString], config[AppConfigurationKeys.HubName], store, null, settings);
                var client = new TaskHubClient(service);
                var instance = await client.CreateOrchestrationInstanceAsync(typeof(FanOutOrchestration), Guid.NewGuid().ToString(), string.Empty);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
            }
            finally
            {
                logger.LogDebug("Stopping application");
            }
        }
    }
}
