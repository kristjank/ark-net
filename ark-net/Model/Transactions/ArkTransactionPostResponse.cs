using ArkNet.Model.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkNet.Model.Transactions
{
    public class ArkTransactionPostResponse : ArkResponseBase
    {
        public List<string> TransactionIds { get; set; }
    }
}
