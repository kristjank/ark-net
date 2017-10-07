using System;
using System.Collections.Generic;
using ArkNet.Core;
using ArkNet.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ArkNet.Model.Transactions;
using ArkNet.Utils;

namespace ArkNet.Service
{
    public static class TransactionService
    {
        public static ArkTransactionList GetAll()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Transaction.GET_ALL);

            return JsonConvert.DeserializeObject<ArkTransactionList>(response);
        }

        public static ArkTransactionList GetUnconfirmedAll()
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Transaction.GET_ALL_UNCONFIRMED);

            return JsonConvert.DeserializeObject<ArkTransactionList>(response);
        }

        public static ArkTransactionResponse GetById(string id)
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Transaction.GET_BY_ID, id));

            return JsonConvert.DeserializeObject<ArkTransactionResponse>(response);
        }

        public static ArkTransactionResponse GetUnConfirmedById(string id)
        {
            var response = NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Transaction.GET_BY_ID_UNCONFIRMED, id));

            return JsonConvert.DeserializeObject<ArkTransactionResponse>(response);
        }

        public static ArkTransactionResponse PostTransaction(TransactionApi transaction)
        {
            string body = "{transactions: [" + transaction.ToObject(true) + "]} ";

            var response = NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.POST, ArkStaticStrings.ArkApiPaths.Transaction.POST, body);

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