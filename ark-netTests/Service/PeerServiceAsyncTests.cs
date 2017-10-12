using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArkNet.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArkNet.Utils.Enum;

namespace ArkNet.Service.Tests
{
    [TestClass()]
    public class PeerServiceAsyncTests
    {
        [TestInitialize]
        public async Task Init()
        {
            await ArkNetApi.Instance.Start(NetworkType.MainNet);
        }

        [TestMethod()]
        public async Task GetAllAsyncTest()
        {
            var peers = await PeerService.GetAllAsync();
            var peer = peers.Peers.Where(x => x.Status.Equals("OK")).FirstOrDefault();

            Assert.IsNotNull(peer);
            Assert.IsNotNull(peers);
        }

        [TestMethod()]
        public async Task GetPeerStatusAsyncTest()
        {
            var peer = await PeerService.GetPeerStatusAsync();

            Assert.IsNotNull(peer);
            Assert.IsNotNull(peer.Header);
            Assert.IsTrue(peer.Success);
            Assert.IsNull(peer.Error);
        }
    }
}