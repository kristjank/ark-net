using System;
using System.Collections.Generic;
using System.Text;

namespace ArkNet.Logging
{
    public enum ArkLogLevel { Debug, Info, Warn, Error, Fatal };

    public class ArkLogEntry
    {
        public readonly ArkLogLevel LogLevel;
        public readonly string Message;
        public readonly Exception Exception;

        public ArkLogEntry(ArkLogLevel logLevel, string message, Exception exception = null)
        {
            LogLevel = logLevel;
            Message = message;
            Exception = exception;
        }
    }
}
