using System;
using ArkNet.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using ArkNet.Model.Account;
using ArkNet.Model.Delegate;
using ArkNet.Utils;

namespace ArkNet.Service
{
    public class AccountService
    {
        public static ArkAccountResponse GetByAddress(string address)
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Account.GET_ACCOUNT, address));

            return JsonConvert.DeserializeObject<ArkAccountResponse>(response);
        }

        public static ArkAccountBalance GetBalance(string address)
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Account.GET_BALANCE, address));

            return JsonConvert.DeserializeObject<ArkAccountBalance>(response);
        }

        public static ArkDelegateList GetDelegates(string address)
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Account.GET_DELEGATES, address));

            return JsonConvert.DeserializeObject<ArkDelegateList>(response);
        }

        public static ArkAccountTopList GetTop(int? limit, int? recordsToSkip)
        {
            var response =
                NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Account.GET_TOP_ACCOUNTS, limit.HasValue ? limit : 100, recordsToSkip.HasValue ? recordsToSkip : 0));

            return JsonConvert.DeserializeObject<ArkAccountTopList>(response);
        }
    }
}