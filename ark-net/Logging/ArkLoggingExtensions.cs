using System;
using System.Collections.Generic;
using System.Text;

namespace ArkNet.Logging
{
    public static class ArkLoggingExtensions
    {
        public static void Log(this IArkLogger logger, string message)
        {
            logger.Log(new ArkLogEntry(ArkLogLevel.Info, message));
        }

        public static void Log(this IArkLogger logger, Exception exception)
        {
            logger.Log(new ArkLogEntry(ArkLogLevel.Error, exception.Message, exception));
        }

        public static void Log(this IArkLogger logger, string message, Exception exception)
        {
            logger.Log(new ArkLogEntry(ArkLogLevel.Error, message, exception));
        }
    }
}
