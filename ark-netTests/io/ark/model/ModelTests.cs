using Microsoft.VisualStudio.TestTools.UnitTesting;
using io.ark.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;
using ark.io.ark.model;
using io.ark.model;

namespace io.ark.model.Tests
{
    [TestClass()]
    public class NetworkTests
    {
       
        [TestMethod()]
        public void GetPeersTest()
        {
            List<PeerVO> peers = Network.Mainnet.GetRandomPeer().GetPeers();
            Assert.IsTrue(peers.Count > 0);
        }

        [TestMethod()]
        public void GetTransactionsTest()
        {
            List<TransactionVO> trans = Network.Mainnet.GetRandomPeer().GetTransactions();
            Assert.IsTrue(trans.Count > 0);
        }

        [TestMethod()]
        public void GetPeerStatusTest()
        {
            PeerStatusVO peerStat = Network.Mainnet.GetRandomPeer().GetPeerStatus();

            Assert.IsNotNull(peerStat);
        }

        [TestMethod()]
        public void GetTransactionTest()
        {
            TransactionVO trans = Network.Mainnet.GetRandomPeer().GetTransaction("3a9643dcf9631384df6cb8c7aec50d782e8da5dfd4b44c22cd1f10c6434ee00c");

            Assert.IsNotNull(trans);
        }

        [TestMethod()]
        public void GetUnconfirmedTransactionsTest()
        {
            List<TransactionVO> trans = Network.Mainnet.GetRandomPeer().GetTransactions(true);
            Assert.IsTrue(trans.Count >= 0);

        }

        [TestMethod()]
        public void GetUnconfirmedTransactionFalseIDTest()
        {
            TransactionVO trans = Network.Mainnet.GetRandomPeer().GetTransaction("WrongID", true);

            Assert.AreEqual(trans.id, "Transaction not found");
        }

        [TestMethod()]
        public void AllDelegatesTest()
        {
            List<DelegateVO> dele = Network.Mainnet.GetRandomPeer().GetDelegates();
            Assert.IsTrue(dele.Count > 0);

            DelegateVO dele1 = Network.Mainnet.GetRandomPeer().GetDelegatebyUsername(dele[0].username);
            Assert.IsNotNull(dele1);

            DelegateVO dele22 = Network.Mainnet.GetRandomPeer().GetDelegatebyPubKey(dele[0].publicKey);
            Assert.IsNotNull(dele1);

            List<DelegateVotersVO> voters = Network.Mainnet.GetRandomPeer().GetDelegateVoters(dele[0].publicKey);
            Assert.IsTrue(voters.Count > 0);
        }
    }
}