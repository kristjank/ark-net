using System;
using System.Collections.Generic;

namespace ArkNet.Utils
{
    public class ArkNetworkSettings
    {
        public int Port { get; set; }
        public byte BytePrefix { get; set; }
        public string Version { get; set; }
        public string NetHash { get; set; }
        public int MaxNumOfBroadcasts { get; set; }
        public Fees Fee { get; set; }
        public IList<string> PeerSeedList { get; set; }

        public ArkNetworkSettings()
        {
            Fee = new Fees();
        }
    }

    public class Fees
    {
        public int Send { get; set; }
        public int Vote { get; set; }
        public long Delegate { get; set; }
        public int SecondSignature { get; set; }
        public int MultiSignature { get; set; }
    }


}