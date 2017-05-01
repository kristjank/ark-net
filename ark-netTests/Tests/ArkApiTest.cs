using System;
using System.Linq;
using ArkNet.API;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArkNetTest.Tests
{
    [TestClass]
    public class ArkApiTest
    {
        [TestMethod]
        public void ArkAPIGetPeers()
        {

            var peer = ArkAPI.Instance.Peers.FirstOrDefault(x => x.Status.Equals("OK"));
            //var p1 = peer.Status
            Assert.IsNotNull(peer);
        }
    }
}
