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
    public class PeerServiceTests
    {
        [TestInitialize]
        public void Init()
        {
            ArkNetApi.Instance.Start(NetworkType.MainNet).Wait();
        }

        [TestMethod()]
        public void GetAllTest()
        {
            var peers = PeerService.GetAll().Peers.Where(x => x.Status.Equals("OK"));
            var peer = peers.FirstOrDefault();

            Assert.IsNotNull(peer);
            Assert.IsNotNull(peers);
        }

        [TestMethod()]
        public void GetPeerStatusTest()
        {
            var peer = PeerService.GetPeerStatus();

            Assert.IsNotNull(peer);
        }
    }
}