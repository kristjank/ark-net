using ArkNet.Model.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkNet.Model.Transactions
{
    public class ArkTransactionList : ArkResponseBase
    {
        public int Count { get; set; }

        public List<ArkTransaction> Transactions { get; set; }
    }
}
