using System.Collections.Generic;
using ArkNet.Core;
using ArkNet.Model;

namespace ArkNet.Service
{
    public static class TransactionService
    {
        public static IEnumerable<ArkTransaction> GetAll() => NetworkApi.Mainnet.ActivePeer.GetTransactions();
        public static IEnumerable<ArkTransaction> GetUnconfirmedAll() => NetworkApi.Mainnet.ActivePeer.GetTransactions(true);
        public static ArkTransaction GetById(string id) => NetworkApi.Mainnet.ActivePeer.GetTransaction(id, false);
        public static ArkTransaction GetUnConfirmedById(string id) => NetworkApi.Mainnet.ActivePeer.GetTransaction(id, true);
    }
}
