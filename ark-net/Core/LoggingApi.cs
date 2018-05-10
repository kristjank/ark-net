using ArkNet.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArkNet.Core
{
    public class LoggingApi
    {
        private IArkLogger _logger;
        public LoggingApi(IArkLogger logger)
        {
            _logger = logger;
        }

        public void Log(ArkLogLevel logLevel, string message)
        {
            if (_logger != null)
                _logger.Log(new ArkLogEntry(logLevel, message));
        }

        public void Log(ArkLogLevel logLevel, Exception exception)
        {
            if (_logger != null)
                _logger.Log(new ArkLogEntry(logLevel, exception.Message, exception));
        }

        public void Log(ArkLogLevel logLevel, string message, Exception exception)
        {
            if (_logger != null)
                _logger.Log(new ArkLogEntry(logLevel, message, exception));
        }
    }
}
