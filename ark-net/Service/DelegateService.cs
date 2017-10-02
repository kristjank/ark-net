using System;
using System.Collections.Generic;
using ArkNet.Core;
using ArkNet.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

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

        public static IEnumerable<ArkDelegateVoter> GetVoters(string pubKey)
        {
            var response =
                NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/delegates/voters?publicKey=" + pubKey);
            var parsed = JObject.Parse(response);
            var array = (JArray) parsed["accounts"];

            var delegVotersList = new List<ArkDelegateVoter>();
            if (!Convert.ToBoolean(parsed["success"]))
            {
                var dele = new ArkDelegateVoter
                {
                    Username = parsed["error"].ToString()
                };
            }
            else
            {
                delegVotersList = JsonConvert.DeserializeObject<List<ArkDelegateVoter>>(array.ToString());
            }
            return delegVotersList;
        }

        public static long GetFee()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/delegates/fee");
            var parsed = JObject.Parse(response);

            return Int64.Parse(parsed["fee"].ToString());
        }

        public static ArkDelegateForgedBalance GetForgedByAccount(string pubKey)
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/delegates/forging/getForgedByAccount?generatorPublicKey=" + pubKey);

            return JsonConvert.DeserializeObject<ArkDelegateForgedBalance>(response);
        }

        public static ArkDelegateNextForgers GetNextForgers()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/delegates/getNextForgers");

            return JsonConvert.DeserializeObject<ArkDelegateNextForgers>(response);
        }

        public static long GetTotalVoteArk(string pubKey)
        {
            return GetVoters(pubKey).Sum(x => x.Balance);
        }
    }
}