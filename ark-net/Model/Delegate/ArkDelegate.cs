using ArkNet.Model.BaseModels;
using System.Collections.Generic;

namespace ArkNet.Model.Delegate
{
    public class ArkDelegate
    {
        public string Username { get; set; }
        public string Address { get; set; }
        public string PublicKey { get; set; }
        public long Vote { get; set; }
        public int Producedblocks { get; set; }
        public int Missedblocks { get; set; }
        public int Rate { get; set; }
        public float Approval { get; set; }
        public float Productivity { get; set; }
    }
}