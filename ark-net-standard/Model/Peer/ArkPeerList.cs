using ArkNet.Model.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkNet.Model.Peer
{
    public class ArkPeerList : ArkResponseBase
    {
        public List<ArkPeer> Peers { get; set; }
    }
}
