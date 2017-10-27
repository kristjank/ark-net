using ArkNet.Model.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkNet.Model.Delegate
{
    public class ArkDelegateVoterList : ArkResponseBase
    {
        public List<ArkDelegateVoter> Accounts { get; set; }
    }
}
