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

        public void Debug(string message)
        {
            if (_logger != null)
                _logger.Log(ArkLogLevel.Debug, message);
        }

        public void Debug(string message, Exception exception)
        {
            if (_logger != null)
                _logger.Log(ArkLogLevel.Debug, message, exception);
        }

        public void Error(string message)
        {
            if (_logger != null)
                _logger.Log(ArkLogLevel.Error, message);
        }

        public void Error(string message, Exception exception)
        {
            if (_logger != null)
                _logger.Log(ArkLogLevel.Error, message, exception);
        }

        public void Fatal(string message)
        {
            if (_logger != null)
                _logger.Log(ArkLogLevel.Fatal, message);
        }

        public void Fatal(string message, Exception exception)
        {
            if (_logger != null)
                _logger.Log(ArkLogLevel.Fatal, message, exception);
        }

        public void Info(string message)
        {
            if (_logger != null)
                _logger.Log(ArkLogLevel.Info, message);
        }

        public void Info(string message, Exception exception)
        {
            if (_logger != null)
                _logger.Log(ArkLogLevel.Info, message, exception);
        }

        public void Warn(string message)
        {
            if (_logger != null)
                _logger.Log(ArkLogLevel.Warn, message);
        }

        public void Warn(string message, Exception exception)
        {
            if (_logger != null)
                _logger.Log(ArkLogLevel.Warn, message, exception);
        }
    }
}
