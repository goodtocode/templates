using DurableTask.Core;
using GoodToCode.Shared.Persistence.StorageTables;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace DurableTask.Activities
{
    public class ProcessDataActivity : TaskActivity<string, bool>
    {
        private readonly IConfiguration config;

        public ProcessDataActivity(IConfiguration configuration)
        {
            config = configuration;
        }

        protected override bool Execute(TaskContext context, string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input));

            // We are passing only IDs due to size limitations
            var dataSourceIds = JsonSerializer.Deserialize<IEnumerable<string>>(input);
            
            // Process the data here
            var processedIds = dataSourceIds.ToList();

            return processedIds.Any();
        }
    }
}
