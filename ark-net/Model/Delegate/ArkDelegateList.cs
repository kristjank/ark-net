using System.Collections.Generic;
using ArkNet.Model.BaseModels;

namespace ArkNet.Model.Delegate
{
    public class ArkDelegateList : ArkResponseBase
    {
        public List<ArkDelegate> Delegates { get; set; }

    }
}
