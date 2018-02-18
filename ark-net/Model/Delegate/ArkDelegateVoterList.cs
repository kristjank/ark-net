using System.Collections.Generic;
using ArkNet.Model.BaseModels;

namespace ArkNet.Model.Delegate
{
    public class ArkDelegateVoterList : ArkResponseBase
    {
        public List<ArkDelegateVoter> Accounts { get; set; }
    }
}
