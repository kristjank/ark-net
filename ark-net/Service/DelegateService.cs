using System;
using System.Collections.Generic;
using ArkNet.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using ArkNet.Model.Delegate;
using ArkNet.Utils;

namespace ArkNet.Service
{
    public static class DelegateService
    {
        public static ArkDelegateList GetAll()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Delegate.GET_ALL);

            return JsonConvert.DeserializeObject<ArkDelegateList>(response);
        }

        public static ArkDelegateResponse GetByUsername(string username)
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Delegate.GET_BY_USERNAME, username));

            return JsonConvert.DeserializeObject<ArkDelegateResponse>(response);
        }

        public static ArkDelegateResponse GetByPubKey(string pubKey)
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Delegate.GET_BY_PUBLIC_KEY, pubKey));

            return JsonConvert.DeserializeObject<ArkDelegateResponse>(response);
        }

        public static ArkDelegateVoterList GetVoters(string pubKey)
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Delegate.GET_VOTERS, pubKey));

            return JsonConvert.DeserializeObject<ArkDelegateVoterList>(response);
        }

        public static long GetFee()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Delegate.GET_FEE);
            var parsed = JObject.Parse(response);

            return Int64.Parse(parsed["fee"].ToString());
        }

        public static ArkDelegateForgedBalance GetForgedByAccount(string pubKey)
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Delegate.GET_FORGED, pubKey));

            return JsonConvert.DeserializeObject<ArkDelegateForgedBalance>(response);
        }

        public static ArkDelegateNextForgers GetNextForgers()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Delegate.GET_NEXT_FORGERS);

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