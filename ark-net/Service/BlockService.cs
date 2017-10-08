using ArkNet.Core;
using ArkNet.Model;
using ArkNet.Model.Block;
using ArkNet.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace ArkNet.Service
{
    public class BlockService
    {
        public static ArkBlockResponse GetById(string id)
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Block.GET_BLOCK, id));

            return JsonConvert.DeserializeObject<ArkBlockResponse>(response);
        }

        public static ArkBlockList GetAll()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_ALL);

            return JsonConvert.DeserializeObject<ArkBlockList>(response);
        }

        public static DateTime GetEpoch()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_EPOCH);
            var parsed = JObject.Parse(response);

            return DateTime.Parse(parsed["epoch"].ToString());
        }

        public static long GetHeight()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_HEIGHT);
            var parsed = JObject.Parse(response);

            return Int64.Parse(parsed["height"].ToString());
        }

        public static string GetNetHash()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_NETHASH);
            var parsed = JObject.Parse(response);

            return parsed["nethash"].ToString();
        }

        public static Fees GetFees()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_FEES);
            var parsed = JObject.Parse(response);

            return JsonConvert.DeserializeObject<Fees>(parsed["fees"].ToString());
        }

        public static int GetMilestone()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_MILESTONE);
            var parsed = JObject.Parse(response);

            return Int32.Parse(parsed["milestone"].ToString());
        }

        public static int GetReward()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_REWARD);
            var parsed = JObject.Parse(response);

            return Int32.Parse(parsed["reward"].ToString());
        }

        public static long GetSupply()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_SUPPLY);
            var parsed = JObject.Parse(response);

            return Int64.Parse(parsed["supply"].ToString());
        }

        public static ArkBlockChainStatus GetStatus()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_STATUS);

            return JsonConvert.DeserializeObject<ArkBlockChainStatus>(response);
        }
    }
}
