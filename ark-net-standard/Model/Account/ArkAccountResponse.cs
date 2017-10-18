using ArkNet.Model.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkNet.Model.Account
{
    public class ArkAccountResponse : ArkResponseBase
    {
        public ArkAccount Account { get; set; }
    }
}
