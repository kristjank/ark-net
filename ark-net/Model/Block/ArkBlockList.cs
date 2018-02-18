using System.Collections.Generic;
using ArkNet.Model.BaseModels;

namespace ArkNet.Model.Block
{
    public class ArkBlockList : ArkResponseBase
    {
        public List<ArkBlock> Blocks { get; set; }
    }
}
