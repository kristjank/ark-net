using ArkNet.Model.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkNet.Model.Account
{
    public class ArkAccountTopList : ArkResponseBase
    {
        public List<ArkAccountTop> Accounts { get; set; }
    }
}
