using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkNet.Utils
{
    public  class ArkSettings
    {

        public class Arkpeer
        {
            public int ConnectionLimit { get; set; }
            public int ConnectionLeaseTimeOut { get; set; }
            public int DefaultConnectionLimit { get; set; }
            public int MaxNumOfBroadcasts { get; set; }
        }

        public class Mainnet
        {
            public int Port { get; set; }
            public int BytePrefix { get; set; }
            public string Version { get; set; }
            public string NetHash { get; set; }
            public Fees Fee { get; set; }
        }

        public class Fees
        {
            public int Send { get; set; }
            public int Vote { get; set; }
            public long Delegate { get; set; }
            public int SecondSignature { get; set; }
            public int MultiSignature { get; set; }
        }

        public class Testnet
        {
            public int Port { get; set; }
            public int BytePrefix { get; set; }
            public string Version { get; set; }
            public string NetHash { get; set; }
            public Fees Fee { get; set; }
        }
    }
}
