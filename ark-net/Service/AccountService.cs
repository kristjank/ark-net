using System;
using ArkNet.Core;
using ArkNet.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
                account.Address = parsed["error"].ToString();
            else
                account = JsonConvert.DeserializeObject<ArkAccount>(parsed["account"].ToString());
            return account;
        }

        public static ArkAccountBalance GetBalance(string address)
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", "/api/accounts/getBalance/?address=" + address);

            return JsonConvert.DeserializeObject<ArkAccountBalance>(response);
        }
    }
}