using DurableTask.Core;
using DurableTask.ServiceBus;
using DurableTask.ServiceBus.Settings;
using DurableTask.ServiceBus.Tracking;
using DurableTask.Activities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    public class FanOutClientFunction
    {
        private readonly IConfiguration config;
        private readonly IServiceProvider serviceProvider;

        public FanOutClientFunction(IConfiguration configuration, IServiceProvider provider)
        {
            config = configuration;
            serviceProvider = provider;
        }

        [Function(FunctionEndpointNames.FanOutClient)]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Link" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "The Internal Service Error response")]
        public async Task<HttpResponseData> RunClient(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequestData req, FunctionContext executionContext)
        {
            string instanceId = Guid.NewGuid().ToString();
            var log = executionContext.GetLogger<FanOutClientFunction>();
            log.LogInformation($"{FunctionEndpointNames.FanOutClient} triggered.");
            HttpResponseData response;

            try
            {
                IOrchestrationServiceInstanceStore store = new AzureTableInstanceStore(config[AppConfigurationKeys.HubName], config[AppConfigurationKeys.StorageTablesConnectionString]);
                var settings = new ServiceBusOrchestrationServiceSettings();
                var service = new ServiceBusOrchestrationService(config[AppConfigurationKeys.ServiceBusConnectionString], config[AppConfigurationKeys.HubName], store, null, settings);
                var client = new TaskHubClient(service);
                var instance = await client.CreateOrchestrationInstanceAsync(typeof(FanOutOrchestration), instanceId, string.Empty);
                var taskResult = await client.WaitForOrchestrationAsync(instance, TimeSpan.FromSeconds(180), CancellationToken.None);
                response = req.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message, ex);
                response = req.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return response;
        }      
    }
}