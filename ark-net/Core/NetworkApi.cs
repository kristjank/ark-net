using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace ArkNet.Core
{
    public sealed class NetworkApi
    {
        private static readonly Lazy<NetworkApi> lazy =
            new Lazy<NetworkApi>(() => new NetworkApi());

        private static readonly Random random = new Random();    
        private readonly List<PeerApi> peers = new List<PeerApi>();
    
        private NetworkApi()
        {
            peers = new List<PeerApi>();
            WarmUp();
        }

        public static NetworkApi Instance => lazy.Value;
   
        public string Nethash { get; set; } = ArkNetApi.Instance.NetworkSettings.NetHash; 
        public string Name { get; set; } = ArkNetApi.Instance.NetworkSettings.Name;
        public int Port { get; set; } = ArkNetApi.Instance.NetworkSettings.Port;
        public byte Prefix { get; set; } = ArkNetApi.Instance.NetworkSettings.BytePrefix;
        public string Version { get; set; } = ArkNetApi.Instance.NetworkSettings.Version;
        public int BroadcastMax { get; set; } = ArkNetApi.Instance.NetworkSettings.MaxNumOfBroadcasts;
        public PeerApi ActivePeer { get; set; }

        private bool WarmUp()
        {
            if (peers.Count > 0) return false;
            foreach (var item in ArkNetApi.Instance.NetworkSettings.PeerSeedList)
                peers.Add(new PeerApi(item));

            ActivePeer = GetRandomPeer();
            return true;
        }

        public PeerApi GetRandomPeer()
        {
            return peers[random.Next(peers.Count())];
        }
    }
}