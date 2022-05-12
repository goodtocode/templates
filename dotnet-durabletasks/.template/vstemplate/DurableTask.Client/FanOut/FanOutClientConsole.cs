using DurableTask.Core;
using DurableTask.ServiceBus;
using DurableTask.ServiceBus.Settings;
using DurableTask.ServiceBus.Tracking;
using DurableTask.Activities;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    public class FanOutClientConsole
    {
        private readonly IConfiguration config;

        public FanOutClientConsole(IConfiguration configuration)
        {
            config = configuration;
        }
        
        public async Task StartAsync()
        {
            string instanceId = Guid.NewGuid().ToString();
            
            IOrchestrationServiceInstanceStore store = new AzureTableInstanceStore(config[AppConfigurationKeys.HubName], config[AppConfigurationKeys.StorageTablesConnectionString]);
            var settings = new ServiceBusOrchestrationServiceSettings();
            var service = new ServiceBusOrchestrationService(config[AppConfigurationKeys.ServiceBusConnectionString], config[AppConfigurationKeys.HubName], store, null, settings);
            var client = new TaskHubClient(service);
            var instance = await client.CreateOrchestrationInstanceAsync(typeof(FanOutOrchestration), instanceId, string.Empty);
            var taskResult = await client.WaitForOrchestrationAsync(instance, TimeSpan.FromSeconds(180), CancellationToken.None);
        }
    }
}

