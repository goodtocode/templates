using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace DurableTask.Tests
{
    public class LoggerFactory
    {
        public static ILogger CreateLogger(LoggerTypes type)
        {
            ILogger logger;

            if (type == LoggerTypes.List)
            {
                logger = new ListLogger();
            }
            else
            {
                logger = NullLoggerFactory.Instance.CreateLogger("Null Logger");
            }

            return logger;
        }

        public static ILogger<T> CreateLogger<T>()
        {
            ILogger<T> logger;
            logger = new GenericLogger<T>();
            return logger;
        }
    }
}
