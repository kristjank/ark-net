using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArkNet.Core;
using ArkNet.Model.Loader;
using ArkNet.Model.Peer;
using ArkNet.Utils;
using ArkNet.Utils.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ArkNet
{
    public sealed class ArkNetApi
    {
        private List<Tuple<string, int>> _peerSeedListMainNet = 
            new List<Tuple<string, int>> {
            Tuple.Create("5.39.9.240", 4001),
            Tuple.Create("5.39.9.241", 4001),
            Tuple.Create("5.39.9.242", 4001),
            Tuple.Create("5.39.9.243", 4001),
            Tuple.Create("5.39.9.244", 4001),
            Tuple.Create("5.39.9.250", 4001),
            Tuple.Create("5.39.9.251", 4001),
            Tuple.Create("5.39.9.252", 4001),
            Tuple.Create("5.39.9.253", 4001),
            Tuple.Create("5.39.9.254", 4001),
            Tuple.Create("5.39.9.255", 4001)
            };

        private List<Tuple<string, int>> _peerSeedListDevNet =
            new List<Tuple<string, int>> {
            Tuple.Create("167.114.43.48", 4002),
            Tuple.Create("167.114.29.49", 4002),
            Tuple.Create("167.114.43.43", 4002),
            Tuple.Create("167.114.29.54", 4002),
            Tuple.Create("167.114.29.45", 4002),
            Tuple.Create("167.114.29.40", 4002),
            Tuple.Create("167.114.29.56", 4002),
            Tuple.Create("167.114.43.35", 4002),
            Tuple.Create("167.114.29.51", 4002),
            Tuple.Create("167.114.29.59", 4002),
            Tuple.Create("167.114.43.42", 4002),
            Tuple.Create("167.114.29.34", 4002),
            Tuple.Create("167.114.29.62", 4002),
            Tuple.Create("167.114.43.49", 4002),
            Tuple.Create("167.114.29.44", 4002)
            };

        private static readonly Lazy<ArkNetApi> _lazy =
            new Lazy<ArkNetApi>(() => new ArkNetApi());

        public static ArkNetApi Instance => _lazy.Value;

        public ArkNetworkSettings NetworkSettings;

        private ArkNetApi()
        {
            
        }

        public async Task Start(NetworkType type)
        {
            await SetNetworkSettings(await GetInitialPeer(type));
        }

        public async Task Start(string initialPeerIp, int initialPeerPort)
        {
            await SetNetworkSettings(GetInitialPeer(initialPeerIp, initialPeerPort));
        }

        private async Task SetNetworkSettings(PeerApi initialPeer)
        {
            var responseAutoConfigure = await initialPeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Loader.GET_AUTO_CONFIGURE);
            var responseFees = await initialPeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_FEES);
            var responsePeer = await initialPeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Peer.GET, initialPeer.Ip, initialPeer.Port));

            var autoConfig = JsonConvert.DeserializeObject<ArkLoaderNetworkResponse>(responseAutoConfigure);
            var fees = JsonConvert.DeserializeObject<Fees>(JObject.Parse(responseFees)["fees"].ToString());
            var peer = JsonConvert.DeserializeObject<ArkPeerResponse>(responsePeer);

            NetworkSettings = new ArkNetworkSettings()
            {
                Port = initialPeer.Port,
                BytePrefix = (byte)autoConfig.Network.Version,
                Version = peer.Peer.Version,
                NetHash = autoConfig.Network.NetHash,
                Fee = fees
            };

            await NetworkApi.Instance.WarmUp(new PeerApi(initialPeer.Ip, initialPeer.Port));
        }

        private PeerApi GetInitialPeer(string initialPeerIp, int initialPeerPort)
        {
            return new PeerApi(initialPeerIp, initialPeerPort);
        }

        private async Task<PeerApi> GetInitialPeer(NetworkType type, int retryCount = 0)
        {
            var peerUrl = _peerSeedListMainNet[new Random().Next(_peerSeedListMainNet.Count)];
            if (type == NetworkType.DevNet)
                peerUrl = _peerSeedListDevNet[new Random().Next(_peerSeedListDevNet.Count)];

            var peer = new PeerApi(peerUrl.Item1, peerUrl.Item2);
            if (await peer.IsOnline())
            {
                return peer;
            }

            if ((type == NetworkType.DevNet && retryCount == _peerSeedListDevNet.Count) 
             || (type == NetworkType.MainNet && retryCount == _peerSeedListMainNet.Count))
                throw new Exception("Unable to connect to a seed peer");

            return await GetInitialPeer(type, retryCount + 1);
        }
    }
}
