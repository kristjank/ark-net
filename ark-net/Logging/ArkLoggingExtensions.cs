using System;
using System.Collections.Generic;
using System.Text;

namespace ArkNet.Logging
{
    public static class ArkLoggingExtensions
    {
        public static void Log(this IArkLogger logger, ArkLogLevel level, string message)
        {
            logger.Log(new ArkLogEntry(level, message));
        }

        public static void Log(this IArkLogger logger, ArkLogLevel level, Exception exception)
        {
            logger.Log(new ArkLogEntry(level, exception.Message, exception));
        }

        public static void Log(this IArkLogger logger, ArkLogLevel level, string message, Exception exception)
        {
            logger.Log(new ArkLogEntry(level, message, exception));
        }
    }
}
