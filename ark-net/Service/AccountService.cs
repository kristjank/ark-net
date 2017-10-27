using System;
using ArkNet.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using ArkNet.Model.Account;
using ArkNet.Model.Delegate;
using ArkNet.Utils;
using System.Threading.Tasks;

namespace ArkNet.Service
{
    public class AccountService
    {
        public static ArkAccountResponse GetByAddress(string address)
        {
            return GetByAddressAsync(address).Result;
        }

        public async static Task<ArkAccountResponse> GetByAddressAsync(string address)
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Account.GET_ACCOUNT, address));

            return JsonConvert.DeserializeObject<ArkAccountResponse>(response);
        }

        public static ArkAccountBalance GetBalance(string address)
        {
            return GetBalanceAsync(address).Result;
        }

        public async static Task<ArkAccountBalance> GetBalanceAsync(string address)
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Account.GET_BALANCE, address));

            return JsonConvert.DeserializeObject<ArkAccountBalance>(response);
        }

        public static ArkDelegateList GetDelegates(string address)
        {
            return GetDelegatesAsync(address).Result;
        }

        public async static Task<ArkDelegateList> GetDelegatesAsync(string address)
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Account.GET_DELEGATES, address));

            return JsonConvert.DeserializeObject<ArkDelegateList>(response);
        }

        public static ArkAccountTopList GetTop(int? limit, int? recordsToSkip)
        {
            return GetTopAsync(limit, recordsToSkip).Result;
        }

        public async static Task<ArkAccountTopList> GetTopAsync(int? limit, int? recordsToSkip)
        {
            var response =
                await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Account.GET_TOP_ACCOUNTS, limit.HasValue ? limit : 100, recordsToSkip.HasValue ? recordsToSkip : 0));

            return JsonConvert.DeserializeObject<ArkAccountTopList>(response);
        }
    }
}