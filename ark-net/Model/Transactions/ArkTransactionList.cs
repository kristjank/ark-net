using System.Collections.Generic;
using ArkNet.Model.BaseModels;

namespace ArkNet.Model.Transactions
{
    public class ArkTransactionList : ArkResponseBase
    {
        public int Count { get; set; }

        public List<ArkTransaction> Transactions { get; set; }
    }
}
