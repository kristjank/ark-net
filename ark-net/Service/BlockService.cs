using ArkNet.Core;
using ArkNet.Model;
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
                block.Id = parsed["error"].ToString();
            else
                block = JsonConvert.DeserializeObject<ArkBlock>(parsed["block"].ToString());
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
    }
}
