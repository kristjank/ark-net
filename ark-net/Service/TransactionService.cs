using System;
using System.Collections.Generic;
using ArkNet.Core;
using ArkNet.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ArkNet.Model.Transactions;
using ArkNet.Utils;
using System.Threading.Tasks;
using System.Linq;
using ArkNet.Messages.Transaction;

namespace ArkNet.Service
{
    public static class TransactionService
    {
        public static ArkTransactionList GetAll()
        {
            return GetAllAsync().Result;
        }

        public async static Task<ArkTransactionList> GetAllAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Transaction.GET_ALL);

            return JsonConvert.DeserializeObject<ArkTransactionList>(response);
        }

        public static ArkTransactionList GetTransactions(ArkTransactionRequest req)
        {
            return GetTransactionsAsync(req).Result;
        }

        public async static Task<ArkTransactionList> GetTransactionsAsync(ArkTransactionRequest req)
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Transaction.GET_ALL + "{0}", req.ToQuery()));

            return JsonConvert.DeserializeObject<ArkTransactionList>(response);
        }

        public static ArkTransactionList GetUnconfirmedAll()
        {
            return GetUnconfirmedAllAsync().Result;
        }

        public async static Task<ArkTransactionList> GetUnconfirmedAllAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Transaction.GET_ALL_UNCONFIRMED);

            return JsonConvert.DeserializeObject<ArkTransactionList>(response);
        }

        public static ArkTransactionList GetUnconfirmedTransactions(ArkUnconfirmedTransactionRequest req)
        {
            return GetUnconfirmedTransactionsAsync(req).Result;
        }

        public async static Task<ArkTransactionList> GetUnconfirmedTransactionsAsync(ArkUnconfirmedTransactionRequest req)
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Transaction.GET_ALL_UNCONFIRMED + "{0}", req.ToQuery()));

            return JsonConvert.DeserializeObject<ArkTransactionList>(response);
        }

        public static ArkTransactionResponse GetById(string id)
        {
            return GetByIdAsync(id).Result;
        }

        public async static Task<ArkTransactionResponse> GetByIdAsync(string id)
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Transaction.GET_BY_ID, id));

            return JsonConvert.DeserializeObject<ArkTransactionResponse>(response);
        }

        public static ArkTransactionResponse GetUnConfirmedById(string id)
        {
            return GetUnConfirmedByIdAsync(id).Result;
        }

        public async static Task<ArkTransactionResponse> GetUnConfirmedByIdAsync(string id)
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Transaction.GET_BY_ID_UNCONFIRMED, id));

            return JsonConvert.DeserializeObject<ArkTransactionResponse>(response);
        }

        public static ArkTransactionPostResponse PostTransaction(TransactionApi transaction, PeerApi peer = null)
        {
            return PostTransactionAsync(transaction, peer).Result;
        }

        public async static Task<ArkTransactionPostResponse> PostTransactionAsync(TransactionApi transaction, PeerApi peer = null)
        {
            string body = "{transactions: [" + transaction.ToObject(true) + "]} ";

            var response = string.Empty;

            if (peer == null)
                response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.POST, ArkStaticStrings.ArkApiPaths.Transaction.POST, body);
            else
                response = await peer.MakeRequest(ArkStaticStrings.ArkHttpMethods.POST, ArkStaticStrings.ArkApiPaths.Transaction.POST, body);

            return JsonConvert.DeserializeObject<ArkTransactionPostResponse>(response);
        }

        public static int MultipleBroadCast(TransactionApi transaction)
        {
            var res = 0;
            var peerURLs = PeerService.GetAll().Peers.Where(x => x.Status.Equals("OK")).Select(x => string.Format("{0}:{1}", x.Ip, x.Port)).ToList();

            for (var i = 0; i < ArkNetApi.Instance.NetworkSettings.MaxNumOfBroadcasts; i++)
            {
                var response = PostTransaction(transaction, new PeerApi(peerURLs[NetworkApi.random.Next(peerURLs.Count)]));

                if (response.Success)
                {
                    res++;
                }
            }

            return res;
        }

        public async static Task<int> MultipleBroadCastAsync(TransactionApi transaction)
        {
            var res = 0;

            var peers = await PeerService.GetAllAsync();
            var peerURLs = peers.Peers.Where(x => x.Status.Equals("OK")).OrderByDescending(x => x.Height).Take(20).Select(x => string.Format("{0}:{1}", x.Ip, x.Port)).ToList();

            for (var i = 0; i < ArkNetApi.Instance.NetworkSettings.MaxNumOfBroadcasts; i++)
            {
                var response = await PostTransactionAsync(transaction, new PeerApi(peerURLs[NetworkApi.random.Next(peerURLs.Count)]));

                if (response.Success)
                {
                    res++;
                }
            }

            return res;
        }
    }
}