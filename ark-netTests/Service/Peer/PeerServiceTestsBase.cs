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
        protected string _ip = "5.39.9.240";
        protected int _port = 4001;

        public void InitializePeerServiceTest()
        {
            base.Initialize();

            Setup();
        }

        public async Task InitializePeerServiceAsyncTest()
        {
            await base.InitializeAsync();

            Setup();
        }

        private void Setup()
        {
            if (base.USE_DEV_NET)
            {
                _ip = "167.114.29.55";
                _port = 4002;
            }
        }

        public void GetPeerResultTest(ArkPeerResponse peer)
        {
            Assert.IsNotNull(peer);
            Assert.IsNotNull(peer.Peer);
            Assert.IsNull(peer.Error);
            Assert.IsTrue(peer.Success);
            Assert.IsTrue(peer.Peer.Ip == _ip);
            Assert.IsTrue(peer.Peer.Port == _port);
        }

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