using ArkNet.Model.BaseModels;

namespace ArkNet.Model.Peer
{
    public class ArkPeerStatus : ArkResponseBase
    {
        public int Height { get; set; }
        public bool ForgingAllowed { get; set; }
        public int CurrentSlot { get; set; }
        public ArkPeerHeader Header { get; set; }
    }
}