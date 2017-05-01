using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArkNet.Core;
using ArkNet.Model;

namespace ArkNet.API
{
    public sealed class ArkAPI
    {

        private static readonly Lazy<ArkAPI> lazy = new Lazy<ArkAPI>(() => new ArkAPI());
        public static ArkAPI Instance { get { return lazy.Value; } }
        
        private ArkAPI()
        {
        }

        public IEnumerable<Peer> Peers => ArkNetwork.Mainnet.ActivePeer.GetPeers();

        
    }
}
