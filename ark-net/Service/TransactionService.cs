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

        public static ArkTransactionList GetUnconfirmedAll()
        {
            return GetUnconfirmedAllAsync().Result;
        }

        public async static Task<ArkTransactionList> GetUnconfirmedAllAsync()
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Transaction.GET_ALL_UNCONFIRMED);

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

        public static ArkTransactionPostResponse PostTransaction(TransactionApi transaction)
        {
            return PostTransactionAsync(transaction).Result;
        }

        public async static Task<ArkTransactionPostResponse> PostTransactionAsync(TransactionApi transaction)
        {
            string body = "{transactions: [" + transaction.ToObject(true) + "]} ";

            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.POST, ArkStaticStrings.ArkApiPaths.Transaction.POST, body);

            return JsonConvert.DeserializeObject<ArkTransactionPostResponse>(response);
        }

        public static int MultipleBroadCast(TransactionApi transaction)
        {
            var res = 0;

            var currentActiveNode = NetworkApi.Instance.ActivePeer;
            var peerURLs = PeerService.GetAll().Peers.Where(x => x.Status.Equals("OK")).Select(x => string.Format("{0}:{1}", x.Ip, x.Port)).ToList();

            for (var i = 0; i < ArkNetApi.Instance.NetworkSettings.MaxNumOfBroadcasts; i++)
            {
                NetworkApi.Instance.ActivePeer = new PeerApi(peerURLs[NetworkApi.random.Next(peerURLs.Count)]);

                var response = PostTransaction(transaction);

                if (response.Success)
                {
                    res++;
                }
            }

            NetworkApi.Instance.ActivePeer = currentActiveNode;

            return res;
        }

        public async static Task<int> MultipleBroadCastAsync(TransactionApi transaction)
        {
            var res = 0;

            var currentActiveNode = NetworkApi.Instance.ActivePeer;
            var peers = await PeerService.GetAllAsync();
            var peerURLs = peers.Peers.Where(x => x.Status.Equals("OK")).Select(x => string.Format("{0}:{1}", x.Ip, x.Port)).ToList();

            for (var i = 0; i < ArkNetApi.Instance.NetworkSettings.MaxNumOfBroadcasts; i++)
            {
                NetworkApi.Instance.ActivePeer = new PeerApi(peerURLs[NetworkApi.random.Next(peerURLs.Count)]);

                var response = await PostTransactionAsync(transaction);

                if (response.Success)
                {
                    res++;
                }
            }

            NetworkApi.Instance.ActivePeer = currentActiveNode;

            return res;
        }
    }
}