using System.Collections.Generic;
using ArkNet.Core;
using ArkNet.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ArkNet.Model.Loader;
using ArkNet.Utils;
using System.Threading.Tasks;

namespace ArkNet.Service
{
    public static class LoaderService
    {
        public static ArkLoaderStatus GetStatus()
        {
            return GetStatusAsync().Result;
        }

        public async static Task<ArkLoaderStatus> GetStatusAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Loader.GET_STATUS);

            return JsonConvert.DeserializeObject<ArkLoaderStatus>(response);
        }

        public static ArkLoaderStatusSync GetSyncStatus()
        {
            return GetSyncStatusAsync().Result;
        }

        public async static Task<ArkLoaderStatusSync> GetSyncStatusAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Loader.GET_SYNC_STATUS);

            return JsonConvert.DeserializeObject<ArkLoaderStatusSync>(response);
        }


        public static ArkLoaderNetworkResponse GetAutoConfigureParameters()
        {
            return GetAutoConfigureParametersAsync().Result;
        }

        public async static Task<ArkLoaderNetworkResponse> GetAutoConfigureParametersAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Loader.GET_AUTO_CONFIGURE);

            return JsonConvert.DeserializeObject<ArkLoaderNetworkResponse>(response);
        }
    }
}