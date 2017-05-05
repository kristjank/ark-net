using System.Collections.Generic;
using ArkNet.Core;
using ArkNet.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ArkNet.Service
{
    public static class PeerService
    {
        public static IEnumerable<ArkPeer> GetAll()
        {
            var response = NetworkApi.Mainnet.ActivePeer.MakeRequest("GET", "/peer/list");
            var parsed = JObject.Parse(response);
            var array = (JArray) parsed["peers"];

            var peerList = JsonConvert.DeserializeObject<IReadOnlyCollection<ArkPeer>>(array.ToString());
            return peerList;
        }

        public static PeerStatus GetPeerStatus()
        {
            var response = NetworkApi.Mainnet.ActivePeer.MakeRequest("GET", "/peer/status");
            var parsed = JObject.Parse(response);

            var peerStat = JsonConvert.DeserializeObject<PeerStatus>(response);
            return peerStat;
        }
    }
}