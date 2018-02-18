using System.Collections.Generic;
using ArkNet.Model.BaseModels;

namespace ArkNet.Model.Account
{
    public class ArkAccountTopList : ArkResponseBase
    {
        public List<ArkAccountTop> Accounts { get; set; }
    }
}
