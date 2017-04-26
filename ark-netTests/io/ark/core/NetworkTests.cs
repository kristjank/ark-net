using Microsoft.VisualStudio.TestTools.UnitTesting;
using io.ark.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace io.ark.core.Tests
{
    [TestClass()]
    public class NetworkTests
    {
        [TestMethod()]
        public void WarmUpTest()
        {
            Network.Mainnet.WarmUp();

            string peers = Network.Mainnet.GetRandomPeer().GetPeers();

            //Dictionary<string,dynamic> peerD = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(peers);

            Assert.IsTrue(peers.Length > 0);
        }

        [TestMethod()]
        public void PostTransactionTest()
        {
            Transaction tx = Transaction.CreateTransaction("AXoXnFi4z1Z6aFvjEYkDVCtBGW2PaRiM25",
                                                           133380000000,
                                                           "This is first transaction from ARK-NET",
                                                           "this is a top secret passphrase");

            Network.Mainnet.WarmUp();
            Peer peer = Network.Mainnet.GetRandomPeer();

            string result = peer.PostTransaction(tx);

            Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(result);

            Assert.AreEqual(jObject["error"], "Account does not have enough ARK: AGeYmgbg2LgGxRW2vNNJvQ88PknEJsYizC balance: 0");
        }
    }
}