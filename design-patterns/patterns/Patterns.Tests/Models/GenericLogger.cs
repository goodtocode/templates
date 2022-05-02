using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace GoodToCode.Shared.Patterns.Tests
{
    public class GenericLogger<T> : ILogger<T>
    {
        public IList<string> Logs;

        public IDisposable BeginScope<TState>(TState state) => NullScope.Instance;

        public bool IsEnabled(LogLevel logLevel) => false;

        public GenericLogger()
        {
            this.Logs = new List<string>();
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string message = formatter(state, exception);
            this.Logs.Add(message);
        }
    }
}
