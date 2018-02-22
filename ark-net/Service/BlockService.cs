using System;
using System.Threading.Tasks;
using ArkNet.Core;
using ArkNet.Messages.Block;
using ArkNet.Model.Block;
using ArkNet.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ArkNet.Service
{
    public class BlockService
    {
        public static ArkBlockResponse GetById(string id)
        {
            return GetByIdAsync(id).Result;
        }
        public async static Task<ArkBlockResponse> GetByIdAsync(string id)
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Block.GET_BLOCK, id));

            return JsonConvert.DeserializeObject<ArkBlockResponse>(response);
        }

        public static ArkBlockList GetAll()
        {
            return GetAllAsync().Result;
        }

        public async static Task<ArkBlockList> GetAllAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_ALL);

            return JsonConvert.DeserializeObject<ArkBlockList>(response);
        }

        public static ArkBlockList GetBlocks(ArkBlockRequest req)
        {
            return GetBlocksAsync(req).Result;
        }

        public async static Task<ArkBlockList> GetBlocksAsync(ArkBlockRequest req)
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Block.GET_ALL + "{0}", req.ToQuery()));

            return JsonConvert.DeserializeObject<ArkBlockList>(response);
        }

        public static DateTime GetEpoch()
        {
            return GetEpochAsync().Result;
        }

        public async static Task<DateTime> GetEpochAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_EPOCH);
            var parsed = JObject.Parse(response);

            return DateTime.Parse(parsed["epoch"].ToString());
        }

        public static long GetHeight()
        {
            return GetHeightAsync().Result;
        }

        public async static Task<long> GetHeightAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_HEIGHT);
            var parsed = JObject.Parse(response);

            return Int64.Parse(parsed["height"].ToString());
        }

        public static string GetNetHash()
        {
            return GetNetHashAsync().Result;
        }

        public async static Task<string> GetNetHashAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_NETHASH);
            var parsed = JObject.Parse(response);

            return parsed["nethash"].ToString();
        }

        public static Fees GetFees()
        {
            return GetFeesAsync().Result;
        }

        public async static Task<Fees> GetFeesAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_FEES);
            var parsed = JObject.Parse(response);

            return JsonConvert.DeserializeObject<Fees>(parsed["fees"].ToString());
        }

        public static int GetMilestone()
        {
            return GetMilestoneAsync().Result;
        }

        public async static Task<int> GetMilestoneAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_MILESTONE);
            var parsed = JObject.Parse(response);

            return Int32.Parse(parsed["milestone"].ToString());
        }

        public static int GetReward()
        {
            return GetRewardAsync().Result;
        }

        public async static Task<int> GetRewardAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_REWARD);
            var parsed = JObject.Parse(response);

            return Int32.Parse(parsed["reward"].ToString());
        }

        public static long GetSupply()
        {
            return GetSupplyAsync().Result;
        }

        public async static Task<long> GetSupplyAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_SUPPLY);
            var parsed = JObject.Parse(response);

            return Int64.Parse(parsed["supply"].ToString());
        }

        public static ArkBlockChainStatus GetStatus()
        {
            return GetStatusAsync().Result;
        }

        public async static Task<ArkBlockChainStatus> GetStatusAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_STATUS);

            return JsonConvert.DeserializeObject<ArkBlockChainStatus>(response);
        }
    }
}
