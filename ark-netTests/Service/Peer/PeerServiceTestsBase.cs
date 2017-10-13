using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArkNet.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArkNet.Utils.Enum;
using ArkNet.Model.Account;
using ArkNet.Model.Delegate;
using ArkNet.Model.Block;
using ArkNet.Utils;
using ArkNet.Model.Loader;
using ArkNet.Model.Peer;
using ArkNet.Tests;

namespace ArkNet.Service.Peer.Tests
{
    public class PeerServiceTestsBase : TestsBase
    {
        public void GetAllResultTest(ArkPeer peer)
        {
            Assert.IsNotNull(peer);
        }

        public void GetPeerStatusResultTest(ArkPeerStatus status)
        {
            Assert.IsNotNull(status);
            Assert.IsNotNull(status.Header);
            Assert.IsTrue(status.Success);
            Assert.IsNull(status.Error);
        }
    }
}