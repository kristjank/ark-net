using System.Collections.Generic;
using ArkNet.Core;
using ArkNet.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ArkNet.Model.Loader;

namespace ArkNet.Service
{
    public static class LoaderService
    {
        public static ArkLoaderStatus GetStatus()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/loader/status");

            return JsonConvert.DeserializeObject<ArkLoaderStatus>(response);
        }

        public static ArkLoaderStatusSync GetSyncStatus()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/loader/status/sync");

            return JsonConvert.DeserializeObject<ArkLoaderStatusSync>(response);
        }

        public static ArkLoaderNetworkResponse GetAutoConfigureParameters()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/loader/autoconfigure");

            return JsonConvert.DeserializeObject<ArkLoaderNetworkResponse>(response);
        }
    }
}