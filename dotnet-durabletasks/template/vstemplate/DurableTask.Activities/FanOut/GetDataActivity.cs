using DurableTask.Core;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace $safeprojectname$
{
    public class GetDataActivity : TaskActivity<string, string>
    {
        private readonly IConfiguration configuration;

        public GetDataActivity(IConfiguration config)
        {
            configuration = config;
        }

        protected override string Execute(TaskContext context, string input)
        {
            // Get IDs of the data to be batched for Fan Out/In
            IEnumerable<string> dataSource = configuration.GetChildren().Select(x => x.Key).ToList();

            var returnSerialized = JsonSerializer.Serialize(dataSource);
            return returnSerialized;
        }
    }
}
