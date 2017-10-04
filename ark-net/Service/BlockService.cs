using ArkNet.Core;
using ArkNet.Model;
using ArkNet.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace ArkNet.Service
{
    public class BlockService
    {
        public static ArkBlock GetById(string id)
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/blocks/get?id=" + id);
            var parsed = JObject.Parse(response);

            var block = new ArkBlock();
            if (!Convert.ToBoolean(parsed["success"]))
            {
                block.Success = false;
                block.Error = parsed["error"].ToString();
            }
            else
            {
                block = JsonConvert.DeserializeObject<ArkBlock>(parsed["block"].ToString());
            }
            return block;
        }

        public static IEnumerable<ArkBlock> GetAll()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/blocks");

            var parsed = JObject.Parse(response);
            var array = (JArray)parsed["blocks"];

            var blockList = JsonConvert.DeserializeObject<IReadOnlyCollection<ArkBlock>>(array.ToString());
            return blockList;
        }

        public static DateTime GetEpoch()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/blocks/getEpoch");
            var parsed = JObject.Parse(response);

            return DateTime.Parse(parsed["epoch"].ToString());
        }

        public static long GetHeight()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/blocks/getHeight");
            var parsed = JObject.Parse(response);

            return Int64.Parse(parsed["height"].ToString());
        }

        public static string GetNetHash()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/blocks/getNethash");
            var parsed = JObject.Parse(response);

            return parsed["nethash"].ToString();
        }

        public static Fees GetFees()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/blocks/getFees");
            var parsed = JObject.Parse(response);

            return JsonConvert.DeserializeObject<Fees>(parsed["fees"].ToString());
        }

        public static int GetMilestone()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/blocks/getMilestone");
            var parsed = JObject.Parse(response);

            return Int32.Parse(parsed["milestone"].ToString());
        }

        public static int GetReward()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/blocks/getReward");
            var parsed = JObject.Parse(response);

            return Int32.Parse(parsed["reward"].ToString());
        }

        public static long GetSupply()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/blocks/getSupply");
            var parsed = JObject.Parse(response);

            return Int64.Parse(parsed["supply"].ToString());
        }

        public static ArkBlockChainStatus GetStatus()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/blocks/getStatus");
            return JsonConvert.DeserializeObject<ArkBlockChainStatus>(response);
        }
    }
}
