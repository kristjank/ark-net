using System.Collections.Generic;
using ArkNet.Core;
using ArkNet.Model;

namespace ArkNet.Service
{
    public static class TransactionService
    {
        public static IEnumerable<Transaction> GetAll() => NetworkApi.Mainnet.ActivePeer.GetTransactions();
        public static IEnumerable<Transaction> GetUnconfirmedAll() => NetworkApi.Mainnet.ActivePeer.GetTransactions(true);
        public static Transaction GetById(string id) => NetworkApi.Mainnet.ActivePeer.GetTransaction(id, false);
        public static Transaction GetUnConfirmedById(string id) => NetworkApi.Mainnet.ActivePeer.GetTransaction(id, true);
    }
}
