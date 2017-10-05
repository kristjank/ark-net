using System;
using System.Collections.Generic;
using ArkNet.Core;
using ArkNet.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ArkNet.Model.Transactions;

namespace ArkNet.Service
{
    public static class TransactionService
    {
        public static ArkTransactionList GetAll()
        {
            var path = "/api/transactions";

            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", path);

            return JsonConvert.DeserializeObject<ArkTransactionList>(response);
        }

        public static ArkTransactionList GetUnconfirmedAll()
        {
            var path = "/api/transactions/unconfirmed";

            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", path);

            return JsonConvert.DeserializeObject<ArkTransactionList>(response);
        }

        public static ArkTransactionResponse GetById(string id)
        {
            var path = "/api/transactions";

            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", path + "/get?id=" + id);

            return JsonConvert.DeserializeObject<ArkTransactionResponse>(response);
        }

        public static ArkTransactionResponse GetUnConfirmedById(string id)
        {
            var path = "/api/transactions/unconfirmed";

            var response = NetworkApi.Instance.ActivePeer.MakeRequest("GET", path + "/get?id=" + id);

            return JsonConvert.DeserializeObject<ArkTransactionResponse>(response);
        }

        public static ArkTransactionResponse PostTransaction(TransactionApi transaction)
        {
            string body = "{transactions: [" + transaction.ToObject(true) + "]} ";

            var response = NetworkApi.Instance.ActivePeer.MakeRequest("POST", "/peer/transactions", body);

            return JsonConvert.DeserializeObject<ArkTransactionResponse>(response);
        }

        public static int MultipleBroadCast(TransactionApi transaction)
        {
            var res = 0;

            for (var i = 0; i < ArkNetApi.Instance.NetworkSettings.MaxNumOfBroadcasts; i++)
            {
                var response = PostTransaction(transaction);

                if (response.Success)
                {
                    res++;
                }
            }

            return res;
        }
    }
}