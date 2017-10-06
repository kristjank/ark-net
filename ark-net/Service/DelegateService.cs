using System;
using System.Collections.Generic;
using ArkNet.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using ArkNet.Model.Delegate;

namespace ArkNet.Service
{
    public static class DelegateService
    {
        public static ArkDelegateList GetAll()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/delegates");

            return JsonConvert.DeserializeObject<ArkDelegateList>(response);
        }

        public static ArkDelegateResponse GetByUsername(string username)
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/delegates/get?username=" + username);

            return JsonConvert.DeserializeObject<ArkDelegateResponse>(response);
        }

        public static ArkDelegateResponse GetByPubKey(string pubKey)
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/delegates/get?publicKey=" + pubKey);

            return JsonConvert.DeserializeObject<ArkDelegateResponse>(response);
        }

        public static ArkDelegateResponse GetByAddress(string address)
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/delegates/get?address=" + address);

            return JsonConvert.DeserializeObject<ArkDelegateResponse>(response);
        }

        public static ArkDelegateVoterList GetVoters(string pubKey)
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/delegates/voters?publicKey=" + pubKey);

            return JsonConvert.DeserializeObject<ArkDelegateVoterList>(response);
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
            var voters = GetVoters(pubKey);

            if (voters.Success)
            {
                return GetVoters(pubKey).Accounts.Sum(x => x.Balance);
            }

            return 0;
        }
    }
}