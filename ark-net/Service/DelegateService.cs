using System;
using System.Collections.Generic;
using ArkNet.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using ArkNet.Model.Delegate;
using ArkNet.Utils;
using System.Threading.Tasks;
using ArkNet.Messages.BaseMessages;

namespace ArkNet.Service
{
    public static class DelegateService
    {
        public static ArkDelegateList GetAll()
        {
            return GetAllAsync().Result;
        }

        public async static Task<ArkDelegateList> GetAllAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Delegate.GET_ALL);

            return JsonConvert.DeserializeObject<ArkDelegateList>(response);
        }

        public static ArkDelegateList GetDelegates(ArkBaseRequest req)
        {
            return GetDelegatesAsync(req).Result;
        }

        public async static Task<ArkDelegateList> GetDelegatesAsync(ArkBaseRequest req)
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Delegate.GET_ALL + "{0}", req.ToQuery()));

            return JsonConvert.DeserializeObject<ArkDelegateList>(response);
        }

        public static ArkDelegateResponse GetByUsername(string username)
        {
            return GetByUsernameAsync(username).Result;
        }

        public async static Task<ArkDelegateResponse> GetByUsernameAsync(string username)
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Delegate.GET_BY_USERNAME, username));

            return JsonConvert.DeserializeObject<ArkDelegateResponse>(response);
        }

        public static ArkDelegateResponse GetByPubKey(string pubKey)
        {
            return GetByPubKeyAsync(pubKey).Result;
        }

        public async static Task<ArkDelegateResponse> GetByPubKeyAsync(string pubKey)
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Delegate.GET_BY_PUBLIC_KEY, pubKey));

            return JsonConvert.DeserializeObject<ArkDelegateResponse>(response);
        }

        public static ArkDelegateVoterList GetVoters(string pubKey)
        {
            return GetVotersAsync(pubKey).Result;
        }

        public async static Task<ArkDelegateVoterList> GetVotersAsync(string pubKey)
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Delegate.GET_VOTERS, pubKey));

            return JsonConvert.DeserializeObject<ArkDelegateVoterList>(response);
        }

        public static long GetFee()
        {
            return GetFeeAsync().Result;
        }

        public async static Task<long> GetFeeAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Delegate.GET_FEE);
            var parsed = JObject.Parse(response);

            return Int64.Parse(parsed["fee"].ToString());
        }

        public static ArkDelegateForgedBalance GetForgedByAccount(string pubKey)
        {
            return GetForgedByAccountAsync(pubKey).Result;
        }

        public async static Task<ArkDelegateForgedBalance> GetForgedByAccountAsync(string pubKey)
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Delegate.GET_FORGED, pubKey));

            return JsonConvert.DeserializeObject<ArkDelegateForgedBalance>(response);
        }

        public static ArkDelegateNextForgers GetNextForgers()
        {
            return GetNextForgersAsync().Result;
        }

        public async static Task<ArkDelegateNextForgers> GetNextForgersAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Delegate.GET_NEXT_FORGERS);

            return JsonConvert.DeserializeObject<ArkDelegateNextForgers>(response);
        }

        public static long GetTotalVoteArk(string pubKey)
        {
            var arkDelegate = GetByPubKey(pubKey);

            if (arkDelegate.Success && arkDelegate.Delegate != null)
            {
                return arkDelegate.Delegate.Vote;
            }

            return 0;
        }

        public async static Task<long> GetTotalVoteArkAsync(string pubKey)
        {
            var arkDelegate = await GetByPubKeyAsync(pubKey);

            if (arkDelegate.Success && arkDelegate.Delegate != null)
            {
                return arkDelegate.Delegate.Vote;
            }

            return 0;
        }
    }
}