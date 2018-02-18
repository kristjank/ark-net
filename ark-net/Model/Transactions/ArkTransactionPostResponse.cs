using System.Collections.Generic;
using ArkNet.Model.BaseModels;

namespace ArkNet.Model.Transactions
{
    public class ArkTransactionPostResponse : ArkResponseBase
    {
        public List<string> TransactionIds { get; set; }
    }
}
