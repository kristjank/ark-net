using System;
using System.Collections.Generic;
using ArkNet.Core;
using ArkNet.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ArkNet.Service
{
    public static class TransactionService
    {
        public static IEnumerable<ArkTransaction> GetAll()
        {
            var path = "/api/transactions";

            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", path);
            var parsed = JObject.Parse(response);
            var array = (JArray) parsed["transactions"];

            var tranList = JsonConvert.DeserializeObject<IReadOnlyCollection<ArkTransaction>>(array.ToString());
            return tranList;
        }

        public static IEnumerable<ArkTransaction> GetUnconfirmedAll()
        {
            var path = "/api/transactions/unconfirmed";

            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", path);
            var parsed = JObject.Parse(response);
            var array = (JArray) parsed["transactions"];

            var tranList = JsonConvert.DeserializeObject<IReadOnlyCollection<ArkTransaction>>(array.ToString());
            return tranList;
        }

        public static ArkTransaction GetById(string id)
        {
            var path = "/api/transactions";

            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", path + "/get?id=" + id);
            var parsed = JObject.Parse(response);

            var trans = new ArkTransaction();
            if (!Convert.ToBoolean(parsed["success"]))
                trans.Id = parsed["error"].ToString();
            else
                trans = JsonConvert.DeserializeObject<ArkTransaction>(parsed["transaction"].ToString());

            return trans;
        }

        public static ArkTransaction GetUnConfirmedById(string id)
        {
            var path = "/api/transactions/unconfirmed";

            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", path + "/get?id=" + id);
            var parsed = JObject.Parse(response);

            var trans = new ArkTransaction();
            if (!Convert.ToBoolean(parsed["success"]))
                trans.Id = parsed["error"].ToString();
            else
                trans = JsonConvert.DeserializeObject<ArkTransaction>(parsed["transaction"].ToString());

            return trans;
        }

        public static (bool status, string data, string error) PostTransaction(TransactionApi transaction)
        {
            string body = "{transactions: [" + transaction.ToObject(true) + "]} ";

            var response = NetworkApi.Instance.ActivePeer.MakeRequest("POST", "/peer/transactions", body);

            var parsed = JObject.Parse(response);
            var status = Convert.ToBoolean(parsed["success"]);


            return new ValueTuple<bool, string, string>(status,
                parsed["message"]?.ToString() ?? parsed["transactionIds"]?.ToString() ?? "",
                parsed["error"]?.ToString() ?? "");
        }

        public static int MultipleBroadCast(TransactionApi transaction)
        {
            var res = 0;
            for (var i = 0; i < ArkNetApi.Instance.NetworkSettings.MaxNumOfBroadcasts; i++)
            {
                var response = PostTransaction(transaction);

                if (response.status)
                    res++;
            }
            return res;
        }
    }
}