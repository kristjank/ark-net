using System;
using System.Collections.Generic;
using ArkNet.Core;
using ArkNet.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ArkNet.Service
{
    public static class DelegateService
    {
        public static IEnumerable<ArkDelegate> GetAll()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/delegates");
            var parsed = JObject.Parse(response);
            var array = (JArray) parsed["delegates"];

            var delegList = JsonConvert.DeserializeObject<IReadOnlyCollection<ArkDelegate>>(array.ToString());
            return delegList;
        }

        public static ArkDelegate GetByUsername(string username)
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/delegates/get?username=" + username);
            var parsed = JObject.Parse(response);

            var dele = new ArkDelegate();
            if (!Convert.ToBoolean(parsed["success"]))
                dele.Username = parsed["error"].ToString();
            else
                dele = JsonConvert.DeserializeObject<ArkDelegate>(parsed["delegate"].ToString());
            return dele;
        }

        public static ArkDelegate GetByPubKey(string pubKey)
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/delegates/get?publicKey=" + pubKey);
            var parsed = JObject.Parse(response);

            var dele = new ArkDelegate();
            if (!Convert.ToBoolean(parsed["success"]))
                dele.Username = parsed["error"].ToString();
            else
                dele = JsonConvert.DeserializeObject<ArkDelegate>(parsed["delegate"].ToString());
            return dele;
        }

        public static ArkDelegate GetByAddress(string address)
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/delegates/get?address=" + address);
            var parsed = JObject.Parse(response);

            var dele = new ArkDelegate();
            if (!Convert.ToBoolean(parsed["success"]))
                dele.Username = parsed["error"].ToString();
            else
                dele = JsonConvert.DeserializeObject<ArkDelegate>(parsed["delegate"].ToString());
            return dele;
        }

        public static IEnumerable<DelegateVoters> GetVoters(string pubKey)
        {
            var response =
                NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/delegates/voters?publicKey=" + pubKey);
            var parsed = JObject.Parse(response);
            var array = (JArray) parsed["accounts"];

            var delegVotersList = new List<DelegateVoters>();
            if (!Convert.ToBoolean(parsed["success"]))
            {
                var dele = new DelegateVoters
                {
                    Username = parsed["error"].ToString()
                };
            }
            else
            {
                delegVotersList = JsonConvert.DeserializeObject<List<DelegateVoters>>(array.ToString());
            }
            return delegVotersList;
        }
    }
}