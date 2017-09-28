using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ArkNet.Model
{
    public class ArkBlockChainStatus
    {
        public DateTime Epoch { get; set; }
        public long Height { get; set; }
        public int Fee { get; set; }
        public int Milestone { get; set; }
        public string NetHash { get; set; }
        public int Reward { get; set; }
        public long Supply { get; set; }
    }
}
