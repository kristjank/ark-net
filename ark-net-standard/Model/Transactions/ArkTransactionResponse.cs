using ArkNet.Model.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkNet.Model.Transactions
{
    public class ArkTransactionResponse : ArkResponseBase
    {
        public ArkTransaction Transaction { get; set; }
    }
}
