using System.Collections.Generic;
using ArkNet.Model.BaseModels;

namespace ArkNet.Model.Peer
{
    public class ArkPeerList : ArkResponseBase
    {
        public List<ArkPeer> Peers { get; set; }
    }
}
