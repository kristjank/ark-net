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

        //private static volatile NetworkApi instance;
        private readonly List<PeerApi> peers = new List<PeerApi>();

        private readonly List<string> peerseed = new List<string>
        {
            "5.39.9.240:4001",
            "5.39.9.241:4001",
            "5.39.9.242:4001",
            "5.39.9.243:4001",
            "5.39.9.244:4001",
            "5.39.9.250:4001",
            "5.39.9.251:4001",
            "5.39.9.252:4001",
            "5.39.9.253:4001",
            "5.39.9.254:4001",
            "5.39.9.255:4001",
            "5.39.53.48:4001",
            "5.39.53.49:4001",
            "5.39.53.50:4001",
            "5.39.53.51:4001",
            "5.39.53.52:4001",
            "5.39.53.53:4001",
            "5.39.53.54:4001",
            "5.39.53.55:4001",
            "37.59.129.160:4001",
            "37.59.129.161:4001",
            "37.59.129.162:4001",
            "37.59.129.163:4001",
            "37.59.129.164:4001",
            "37.59.129.165:4001",
            "37.59.129.166:4001",
            "37.59.129.167:4001",
            "37.59.129.168:4001",
            "37.59.129.169:4001",
            "37.59.129.170:4001",
            "37.59.129.171:4001",
            "37.59.129.172:4001",
            "37.59.129.173:4001",
            "37.59.129.174:4001",
            "37.59.129.175:4001",
            "193.70.72.80:4001",
            "193.70.72.81:4001",
            "193.70.72.82:4001",
            "193.70.72.83:4001",
            "193.70.72.84:4001",
            "193.70.72.85:4001",
            "193.70.72.86:4001",
            "193.70.72.87:4001",
            "193.70.72.88:4001",
            "193.70.72.89:4001",
            "193.70.72.90:4001"
        };


        private NetworkApi()
        {
            peers = new List<PeerApi>();
            WarmUp();
        }

        public static NetworkApi Instance => lazy.Value;

        /*public static NetworkApi Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NetworkApi();
                    instance.WarmUp();
                }
            
                return instance;
            }
        }*/

        public string Nethash { get; set; } =
            ArkNetApi.Instance.NetworkSettings
                .NetHash; //"6e84d08bd299ed97c212c886c98a57e36545c8f5d645ca7eeae63a8bd62d8988";

        public string Name { get; set; } = ArkNetApi.Instance.NetworkSettings.Name;
        public int Port { get; set; } = ArkNetApi.Instance.NetworkSettings.Port;
        public byte Prefix { get; set; } = ArkNetApi.Instance.NetworkSettings.BytePrefix;
        public string Version { get; set; } = ArkNetApi.Instance.NetworkSettings.Version;
        public int BroadcastMax { get; set; } = ArkNetApi.Instance.NetworkSettings.MaxNumOfBroadcasts;
        public PeerApi ActivePeer { get; set; }

        public dynamic GetHeaders(bool retJson = false)
        {
            var data = new Dictionary<string, dynamic> {["nethash"] = Nethash, ["version"] = Version, ["port"] = Port};

            if (retJson)
                return JsonConvert.SerializeObject(data);
            return data;
        }

        private bool WarmUp()
        {
            if (peers.Count > 0) return false;
            foreach (var item in peerseed)
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