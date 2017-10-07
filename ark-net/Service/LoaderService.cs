using System.Collections.Generic;
using ArkNet.Core;
using ArkNet.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ArkNet.Model.Loader;
using ArkNet.Utils;

namespace ArkNet.Service
{
    public static class LoaderService
    {
        public static ArkLoaderStatus GetStatus()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Loader.GET_STATUS);

            return JsonConvert.DeserializeObject<ArkLoaderStatus>(response);
        }

        public static ArkLoaderStatusSync GetSyncStatus()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Loader.GET_SYNC_STATUS);

            return JsonConvert.DeserializeObject<ArkLoaderStatusSync>(response);
        }

        public static ArkLoaderNetworkResponse GetAutoConfigureParameters()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Loader.GET_AUTO_CONFIGURE);

            return JsonConvert.DeserializeObject<ArkLoaderNetworkResponse>(response);
        }
    }
}