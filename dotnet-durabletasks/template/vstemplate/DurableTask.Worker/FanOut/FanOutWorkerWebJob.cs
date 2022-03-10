using DurableTask.Core;
using DurableTask.ServiceBus;
using DurableTask.ServiceBus.Settings;
using DurableTask.ServiceBus.Tracking;
using DurableTask.Activities;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    public class FanOutWorkerWebJob
    {
        private const int timeout = 180;
        private readonly IConfiguration config;
        private readonly ILogger log;
        private readonly IServiceProvider serviceProvider;
        public FanOutWorkerWebJob(IConfiguration configuration, ILogger<FanOutWorkerWebJob> logger, IServiceProvider provider)
        {
            log = logger;
            config = configuration;
            serviceProvider = provider;
        }

        public async Task RunWorker(
            [TimerTrigger("0 */3 * * * *")] TimerInfo timer)
        {
            log.LogInformation($"{GetType().FullName} triggered.");

            try
            {
                IOrchestrationServiceInstanceStore store = new AzureTableInstanceStore(config[AppConfigurationKeys.HubName], config[AppConfigurationKeys.StorageTablesConnectionString]);
                var settings = new ServiceBusOrchestrationServiceSettings();
                var service = new ServiceBusOrchestrationService(config[AppConfigurationKeys.ServiceBusConnectionString], config[AppConfigurationKeys.HubName], store, null, settings);
                TaskHubWorker hubWorker = new TaskHubWorker(service)
                    .AddTaskOrchestrations(new ServiceProviderObjectCreator<TaskOrchestration>(typeof(FanOutOrchestration), serviceProvider))
                    .AddTaskActivities(new ServiceProviderObjectCreator<TaskActivity>(typeof(GetDataActivity), serviceProvider))
                    .AddTaskActivities(new ServiceProviderObjectCreator<TaskActivity>(typeof(ProcessDataActivity), serviceProvider));
                await service.CreateIfNotExistsAsync();
                await hubWorker.StartAsync();
                for(int count = 0; count < timeout; count++)
                {
                    Thread.Sleep(1000);
                }
                //hubWorker.StopAsync(true).Wait();

            }
            catch (Exception ex)
            {
                log.LogError(ex.Message, ex);
            }
        }
    }
}