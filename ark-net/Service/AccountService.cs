using System;
using ArkNet.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using ArkNet.Model.Account;
using ArkNet.Model.Delegate;

namespace ArkNet.Service
{
    public class AccountService
    {
        public static ArkAccountResponse GetByAddress(string address)
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/accounts/?address=" + address);

            return JsonConvert.DeserializeObject<ArkAccountResponse>(response);
        }

        public static ArkAccountBalance GetBalance(string address)
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/accounts/getBalance/?address=" + address);

            return JsonConvert.DeserializeObject<ArkAccountBalance>(response);
        }

        public static ArkDelegateList GetDelegates(string address)
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/accounts/delegates?address=" + address);

            return JsonConvert.DeserializeObject<ArkDelegateList>(response);
        }

        public static ArkAccountTopList GetTop(int? limit, int? recordsToSkip)
        {
            var response =
                NetworkApi.Instance.ActivePeer.MakeRequest("GET", string.Format("/api/accounts/top?limit={0}&offset={1}", limit.HasValue ? limit : 100, recordsToSkip.HasValue ? recordsToSkip : 0));

            return JsonConvert.DeserializeObject<ArkAccountTopList>(response);
        }
    }
}