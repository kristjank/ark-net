using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArkNet.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArkNet.Utils.Enum;

namespace ArkNet.Service.Peer.Tests
{
    [TestClass()]
    public class PeerServiceTests : PeerServiceTestsBase
    {
        [TestInitialize]
        public void Init()
        {
            base.Initialize();
        }

        [TestMethod()]
        public void GetAllTest()
        {
            var peers = PeerService.GetAll().Peers.Where(x => x.Status.Equals("OK"));
            var peer = peers.FirstOrDefault();

            GetAllResultTest(peer);
        }

        [TestMethod()]
        public void GetPeerStatusTest()
        {
            var peer = PeerService.GetPeerStatus();

            GetPeerStatusResultTest(peer);
        }
    }
}