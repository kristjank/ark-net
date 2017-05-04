using System.Collections.Generic;
using ArkNet.Core;
using ArkNet.Model;

namespace ArkNet.Service
{
    public static class PeerService
    {
        public static IEnumerable<ArkPeer> GetAll() => NetworkApi.Mainnet.ActivePeer.GetPeers();
    }
}
