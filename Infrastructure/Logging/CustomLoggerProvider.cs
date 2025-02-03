using Microsoft.Extensions.Logging;

namespace Vidya.Infrastructure.Logging
{
    public class CustomLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new CustomLogger();
        }

        public void Dispose()
        {
            // Dispose resources if any
        }
    }
}
