using System;
using System.Collections.Generic;

namespace ArkNet.Utils
{
    public class ArkNetworkSettings
    {
        public int Port { get; set; }
        public byte PubKeyHash { get; set; }
        public byte WifPrefix { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string NetHash { get; set; }
        public int MaxNumOfBroadcasts { get; set; }
        public Fees Fee { get; set; }
        public IList<string> PeerSeedList { get; set; }

        public ArkNetworkSettings(dynamic activeNet)
        {
            Fee = new Fees();
            this.Port = activeNet.Port;
            this.PubKeyHash = Convert.ToByte(activeNet.PubKeyHash, 16);
            this.WifPrefix = Convert.ToByte(activeNet.WifPrefix, 16);
            this.NetHash = activeNet.NetHash;
            this.Version = activeNet.Version;
            this.Name = activeNet.Name;
            this.MaxNumOfBroadcasts = activeNet.MaxNumOfBroadcasts;
            this.Fee.Send = activeNet.Fees.Send;
            this.Fee.Vote = activeNet.Fees.Vote;
            this.Fee.Delegate = activeNet.Fees.Delegate;
            this.Fee.SecondSignature = activeNet.Fees.SecondSignature;
            this.Fee.MultiSignature = activeNet.Fees.MultiSignature;
            this.PeerSeedList = activeNet.Peers;
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