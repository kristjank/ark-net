using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArkNet.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArkNet.Utils.Enum;
using ArkNet.Core;

namespace ArkNet.Service.Peer.Tests
{
    [TestClass()]
    public class PeerServiceTests : PeerServiceTestsBase
    {
        [TestInitialize]
        public void Init()
        {
            base.InitializePeerServiceTest();
        }

        [TestMethod()]
        public void GetAllTest()
        {
            var peers = ArkNetApi.PeerService.GetAll().Peers.Where(x => x.Status.Equals("OK"));
            var peer = peers.FirstOrDefault();

            GetAllResultTest(peer);
        }

        [TestMethod()]
        public void GetPeerTest()
        {
            var peer = ArkNetApi.PeerService.GetPeer(base._ip, base._port);

            GetPeerResultTest(peer);
        }

        [TestMethod()]
        public void GetPeerStatusTest()
        {
            var peer = ArkNetApi.PeerService.GetPeerStatus();

            GetPeerStatusResultTest(peer);
        }

        [TestMethod]
        public void SwitchPeerTest()
        {
            ArkNetApi.NetworkApi.ActivePeer = new PeerApi(ArkNetApi, "1.1.1.1", 5000);

            var peer = ArkNetApi.PeerService.GetPeer(base._ip, base._port);

            GetPeerResultTest(peer);
        }
    }
}