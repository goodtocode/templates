using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    public class HealthcheckFunction
    {
        private readonly ILogger log;

        public HealthcheckFunction(ILogger<HealthcheckFunction> logger)
        {
            log = logger;
        }

        [Function(FunctionEndpointNames.Healthcheck)]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Health" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequestData req)
        {
            log.LogInformation($"{FunctionEndpointNames.Healthcheck} triggered.");
            var health = new FunctionHealth() { Connected = true };

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync<FunctionHealth>(health);

            return response;
        }
    }
}
