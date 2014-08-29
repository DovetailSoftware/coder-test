using System;

namespace InteractionTests
{
    public interface ILoggingService
    {
        void LogDebug(string logMessage);
        void LogError(string errorMessage, Exception exception);
    }
}