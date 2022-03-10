using DurableTask.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    public class FanOutOrchestration : TaskOrchestration<bool, string>
    {
        private const string batchSizeDefault = "100";

        private readonly ILogger log;
        private readonly RetryOptions options;
        private readonly int batchSize;

        public FanOutOrchestration(ILogger<FanOutOrchestration> logger, IConfiguration config)
        {
            log = logger;
            batchSize = Convert.ToInt32(config[AppConfigurationKeys.BatchSize] ?? batchSizeDefault);
            options = new RetryOptions(TimeSpan.FromSeconds(1), 5) { BackoffCoefficient = 1.1, Handle = HandleError };
        }

        public override async Task<bool> RunTask(OrchestrationContext context, string input)
        {
            string dataSourceSerialized;
            IEnumerable<IEnumerable<string>> batches;
            var tasks = new List<Task<bool>>();

            // Get IDs of data to be batched and processed
            dataSourceSerialized = await context.ScheduleWithRetry<string>(typeof(GetDataActivity), options, string.Empty);

            // Break up data into batches
            batches = JsonSerializer.Deserialize<IEnumerable<string>>(dataSourceSerialized).ToBatches(batchSize);

            // Fan Out each batch activity to process
            foreach (var batch in batches)
            {
                var batchSerialized = JsonSerializer.Serialize(batch);
                tasks.Add(context.ScheduleWithRetry<bool>(typeof(ProcessDataActivity), options, batchSerialized));
            }
            
            // Begin activities and Fan In when done
            await Task.WhenAll(tasks);

            return true;
        }

        private bool HandleError(Exception e)
        {
            log.LogError(e, e.Message);
            return false;
        }
    }
}
