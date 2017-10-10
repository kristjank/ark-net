using System.Collections.Generic;
using ArkNet.Core;
using ArkNet.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ArkNet.Model.Peer;
using ArkNet.Utils;
using System.Threading.Tasks;

namespace ArkNet.Service
{
    public static class PeerService
    {
        public static ArkPeerList GetAll()
        {
            return GetAllAsync().Result;
        }

        public async static Task<ArkPeerList> GetAllAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Peer.GET_ALL);

            return JsonConvert.DeserializeObject<ArkPeerList>(response);
        }

        public static ArkPeerStatus GetPeerStatus()
        {
            return GetPeerStatusAsync().Result;
        }

        public async static Task<ArkPeerStatus> GetPeerStatusAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Peer.GET_STATUS);

            return JsonConvert.DeserializeObject<ArkPeerStatus>(response);
        }
    }
}