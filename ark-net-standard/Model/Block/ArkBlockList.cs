using ArkNet.Model.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkNet.Model.Block
{
    public class ArkBlockList : ArkResponseBase
    {
        public List<ArkBlock> Blocks { get; set; }
    }
}
