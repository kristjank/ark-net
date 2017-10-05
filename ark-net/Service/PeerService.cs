using System.Collections.Generic;
using ArkNet.Core;
using ArkNet.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ArkNet.Model.Peer;

namespace ArkNet.Service
{
    public static class PeerService
    {
        public static ArkPeerList GetAll()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/peer/list");

            return JsonConvert.DeserializeObject<ArkPeerList>(response);
        }

        public static ArkPeerStatus GetPeerStatus()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/peer/status");

            return JsonConvert.DeserializeObject<ArkPeerStatus>(response);
        }
    }
}