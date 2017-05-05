using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArkNet.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkNet.Service.Tests
{
    [TestClass()]
    public class PeerServiceTests
    {
        [TestMethod()]
        public void GetAllTest()
        {
            var peers = PeerService.GetAll().Where(x => x.Status.Equals("OK"));
            var peer = peers.FirstOrDefault();

            Assert.IsNotNull(peer);
            Assert.IsNotNull(peers);
        }
    }
}