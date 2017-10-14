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
    public class PeerServiceAsyncTests : PeerServiceTestsBase
    {
        [TestInitialize]
        public async Task Init()
        {
            await base.InitializeAsync();
        }

        [TestMethod()]
        public async Task GetAllAsyncTest()
        {
            var peers = await PeerService.GetAllAsync();
            var peer = peers.Peers.Where(x => x.Status.Equals("OK")).FirstOrDefault();

            GetAllResultTest(peer);
        }

        [TestMethod()]
        public async Task GetPeerStatusAsyncTest()
        {
            var peer = await PeerService.GetPeerStatusAsync();

            GetPeerStatusResultTest(peer);
        }
    }
}