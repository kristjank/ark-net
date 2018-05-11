using System;
using System.Collections.Generic;
using System.Text;

namespace ArkNet.Logging
{
    public interface IArkLogger
    {
        void Log(ArkLogEntry logEntry);
    }
}
