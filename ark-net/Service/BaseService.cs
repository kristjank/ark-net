using ArkNet.Core;
using ArkNet.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArkNet.Service
{
    public class BaseService
    {
        protected NetworkApi _networkApi;
        protected LoggingApi _logger;
        public BaseService(NetworkApi networkApi, LoggingApi logger)
        {
            _networkApi = networkApi;
            _logger = logger;
        }
    }
}
