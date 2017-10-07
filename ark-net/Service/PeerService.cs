using System.Collections.Generic;
using ArkNet.Core;
using ArkNet.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ArkNet.Model.Peer;
using ArkNet.Utils;

namespace ArkNet.Service
{
    public static class PeerService
    {
        public static ArkPeerList GetAll()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Peer.GET_ALL);

            return JsonConvert.DeserializeObject<ArkPeerList>(response);
        }

        public static ArkPeerStatus GetPeerStatus()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Peer.GET_STATUS);

            return JsonConvert.DeserializeObject<ArkPeerStatus>(response);
        }
    }
}