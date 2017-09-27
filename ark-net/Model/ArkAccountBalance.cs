using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkNet.Model
{
    public class ArkAccountBalance
    {
        public bool Success { get; set; }
        public long Balance { get; set; }
        public long UnconfirmedBalance { get; set; }
    }
}
