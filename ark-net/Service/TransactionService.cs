using System.Collections.Generic;
using System.Threading.Tasks;
using ArkNet.Core;
using ArkNet.Messages.Transaction;
using ArkNet.Model.Transactions;
using ArkNet.Utils;
using Newtonsoft.Json;

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

        public static ArkTransactionList GetTransactions(string address, int offset = 0, int limit = 50)
        {
            return GetTransactions(new ArkTransactionRequest { OrderBy = "timestamp:desc", RecipientId = address, SenderId = address, Offset = offset, Limit = limit });
        }

        public async static Task<ArkTransactionList> GetTransactionsAsync(string address, int offset = 0, int limit = 50)
        {
            return await GetTransactionsAsync(new ArkTransactionRequest { OrderBy = "timestamp:desc", RecipientId = address, SenderId = address, Offset = offset, Limit = limit });
        }

        public static ArkTransactionList GetUnconfirmedTransactions(string address)
        {
            return GetUnconfirmedTransactions(new ArkUnconfirmedTransactionRequest { Address = address });
        }

        public async static Task<ArkTransactionList> GetUnconfirmedTransactionsAsync(string address)
        {
            return await GetUnconfirmedTransactionsAsync(new ArkUnconfirmedTransactionRequest { Address = address });
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

        public static List<ArkTransactionPostResponse> MultipleBroadCast(TransactionApi transaction)
        {
            var res = new List<ArkTransactionPostResponse>();

            for (var i = 0; i < ArkNetApi.Instance.NetworkSettings.MaxNumOfBroadcasts; i++)
            {
                res.Add(PostTransaction(transaction, NetworkApi.Instance.GetRandomPeer()));
            }

            return res;
        }

        public async static Task<List<ArkTransactionPostResponse>> MultipleBroadCastAsync(TransactionApi transaction)
        {
            var res = new List<ArkTransactionPostResponse>();

            for (var i = 0; i < ArkNetApi.Instance.NetworkSettings.MaxNumOfBroadcasts; i++)
            {
                res.Add(await PostTransactionAsync(transaction, NetworkApi.Instance.GetRandomPeer()));
            }

            return res;
        }
    }
}