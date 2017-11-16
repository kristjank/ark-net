using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ArkNet.Core
{
    public sealed class NetworkApi
    {
        private static readonly Lazy<NetworkApi> lazy =
            new Lazy<NetworkApi>(() => new NetworkApi());
  
        private readonly List<PeerApi> peers = new List<PeerApi>();
    
        private NetworkApi()
        {
            peers = new List<PeerApi>();
        }

        public static readonly Random random = new Random();
        public static NetworkApi Instance => lazy.Value;
   
        public string Nethash { get; set; } = ArkNetApi.Instance.NetworkSettings.NetHash; 
        public int Port { get; set; } = ArkNetApi.Instance.NetworkSettings.Port;
        public byte Prefix { get; set; } = ArkNetApi.Instance.NetworkSettings.BytePrefix;
        public string Version { get; set; } = ArkNetApi.Instance.NetworkSettings.Version;
        public int BroadcastMax { get; set; } = ArkNetApi.Instance.NetworkSettings.MaxNumOfBroadcasts;
        public PeerApi ActivePeer { get; set; }

        public void WarmUp(PeerApi initialPeer)
        {
            peers.Add(initialPeer);
            ActivePeer = initialPeer;
        }

        public PeerApi GetRandomPeer()
        {
            return peers[random.Next(peers.Count())];
        }
    }
}