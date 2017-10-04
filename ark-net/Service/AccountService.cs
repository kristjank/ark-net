using System;
using ArkNet.Core;
using ArkNet.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ArkNet.Service
{
    public class AccountService
    {
        public static ArkAccount GetByAddress(string address)
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/accounts/?address=" + address);
            var parsed = JObject.Parse(response);

            var account = new ArkAccount();
            if (!Convert.ToBoolean(parsed["success"]))
            {
                account.Success = false;
                account.Error = parsed["error"].ToString();
            }
            else
            {
                account = JsonConvert.DeserializeObject<ArkAccount>(parsed["account"].ToString());
            }
            return account;
        }

        public static ArkAccountBalance GetBalance(string address)
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/accounts/getBalance/?address=" + address);

            return JsonConvert.DeserializeObject<ArkAccountBalance>(response);
        }

        public static ArkDelegateCollection GetDelegates(string address)
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/accounts/delegates?address=" + address);

            return JsonConvert.DeserializeObject<ArkDelegateCollection>(response);
        }

        public static ArkAccountTopCollection GetTop(int? limit, int? recordsToSkip)
        {
            var response =
                NetworkApi.Instance.ActivePeer.MakeRequest("GET", string.Format("/api/accounts/top?limit={0}&offset={1}", limit.HasValue ? limit : 100, recordsToSkip.HasValue ? recordsToSkip : 0));

            return JsonConvert.DeserializeObject<ArkAccountTopCollection>(response);
        }
    }
}