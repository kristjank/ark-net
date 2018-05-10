using System.IO;
using ArkNet;
using ArkNet.Core;
using ArkNet.Service;
using ArkNet.Utils;
using ArkNet.Utils.Enum;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArkNet.Tests;
using System.Threading.Tasks;
using System.Linq;
using ArkNet.Logging;
using log4net;

namespace ArkNetTest.Tests
{
    [TestClass]
	public class LoggingTests : TestsBase
	{
        public class Log4netAdapter : IArkLogger
        {
            private readonly ILog _log4NetLog;

            public Log4netAdapter(ILog log4NetLog)
            {
                _log4NetLog = log4NetLog;
            }

            public void Log(ArkLogEntry entry)
            {
                if (entry.LogLevel == ArkLogLevel.Debug)
                    _log4NetLog.Debug(entry.Message, entry.Exception);
                else if (entry.LogLevel == ArkLogLevel.Info)
                    _log4NetLog.Info(entry.Message, entry.Exception);
                else if (entry.LogLevel == ArkLogLevel.Warn)
                    _log4NetLog.Warn(entry.Message, entry.Exception);
                else if (entry.LogLevel == ArkLogLevel.Error)
                    _log4NetLog.Error(entry.Message, entry.Exception);
                else
                    _log4NetLog.Fatal(entry.Message, entry.Exception);
            }
        }

        [TestInitialize]
	    public void Init()
	    {
            ILog log = LogManager.GetLogger(typeof(LoggingTests));
            base.Initialize(new Log4netAdapter(log));
	    }

        [TestMethod]
        public void Log()
        {
            ArkNetApi.LoggingApi.Debug("Test Log");
            ArkNetApi.LoggingApi.Info("Test Log");
            ArkNetApi.LoggingApi.Warn("Test Log");
            ArkNetApi.LoggingApi.Error("Test Log");
            ArkNetApi.LoggingApi.Fatal("Test Log");
        }
    }
}