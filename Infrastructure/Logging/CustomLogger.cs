using Microsoft.Extensions.Logging;
using System;

namespace Vidya.Infrastructure.Logging
{
    public class CustomLogger : ILogger
    {
        // Implementing the Log method
        public IDisposable BeginScope<TState>(TState state)
        {
            // Return a scope (you can implement it as needed)
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            // Determine if the log level is enabled (e.g., based on configuration)
            return logLevel >= LogLevel.Information; // For example, logging Information and above
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception?, string> formatter)
        {
            // Log the message with the appropriate log level
            if (formatter != null)
            {
                string message = formatter(state, exception);
                Console.WriteLine($"[{logLevel}] {message}");
            }
        }
    }
}
