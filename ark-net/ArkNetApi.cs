using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArkNet.Utils;
using ArkNet.Utils.Enum;
using NBitcoin.DataEncoders;
using ArkNet.Core;
using Newtonsoft.Json;
using ArkNet.Model.Loader;
using Newtonsoft.Json.Linq;
using ArkNet.Model.Peer;

namespace ArkNet
{
    public sealed class ArkNetApi
    {
        private static readonly Lazy<ArkNetApi> lazy =
            new Lazy<ArkNetApi>(() => new ArkNetApi());

        public static ArkNetApi Instance => lazy.Value;

        public ArkNetworkSettings NetworkSettings;

        private ArkNetApi()
        {
            
        }

        public async Task Start(NetworkType type)
        {
            var initialPeer = new PeerApi("5.39.9.240:4001");
            if (type == NetworkType.DevNet)
                initialPeer = new PeerApi("167.114.29.55:4002");

            await SetNetworkSettings(initialPeer);
            await NetworkApi.Instance.WarmUp();
        }

        public async Task Start(ArkNetworkSettings settings)
        {
            NetworkSettings = settings;
            await NetworkApi.Instance.WarmUp();
        }

        public async Task Start(string initialPeerIp, int initialPeerPort)
        {
            await SetNetworkSettings(GetInitialPeer(initialPeerIp, initialPeerPort));
            await NetworkApi.Instance.WarmUp();
        }

        private async Task SetNetworkSettings(PeerApi initialPeer)
        {
            var responseAutoConfigure = await initialPeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Loader.GET_AUTO_CONFIGURE);
            var responseFees = await initialPeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_FEES);
            var responsePeer = await initialPeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Peer.GET, initialPeer.ip, initialPeer.port));

            var autoConfig = JsonConvert.DeserializeObject<ArkLoaderNetworkResponse>(responseAutoConfigure);
            var fees = JsonConvert.DeserializeObject<Fees>(JObject.Parse(responseFees)["fees"].ToString());
            var peer = JsonConvert.DeserializeObject<ArkPeerResponse>(responsePeer);

            NetworkSettings = new ArkNetworkSettings()
            {
                Port = initialPeer.port,
                BytePrefix = (byte)autoConfig.Network.Version,
                Version = peer.Peer.Version,
                NetHash = autoConfig.Network.NetHash,
                MaxNumOfBroadcasts = 5,
                Fee = fees,
                PeerSeedList = new List<string>
                    {
                        string.Format("{0}:{1}", initialPeer.ip, initialPeer.port)
                    }
            };
        }

        private PeerApi GetInitialPeer(string initialPeerIp, int initialPeerPort)
        {
            return new PeerApi(string.Format("{0}:{1}", initialPeerIp, initialPeerPort));
        }
    }
}
