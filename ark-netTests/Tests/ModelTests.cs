using System.Linq;
using ArkNet.Core;
using ArkNet.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArkNetTest.Tests
{
	[TestClass]
	public class ModelApiVOTests
	{
		[TestMethod]
		public void GetPeersTest()
		{
			var peers = ArkNetwork.Mainnet.GetRandomPeer().GetPeers();


		    var peer = peers.Where(x => x.Status.Equals("OK")).FirstOrDefault();
//		    var peer = peersOk.;

            Assert.IsNotNull(peer);

		}

		[TestMethod]
		public void GetTransactionsTest()
		{
			var trans = ArkNetwork.Mainnet.GetRandomPeer().GetTransactions();
			Assert.IsTrue(trans.Count > 0);
		}

		[TestMethod]
		public void GetPeerStatusTest()
		{
			var peerStat = ArkNetwork.Mainnet.GetRandomPeer().GetPeerStatus();

			Assert.IsNotNull(peerStat);
		}

		[TestMethod]
		public void GetTransactionTest()
		{
			var trans = ArkNetwork.Mainnet.GetRandomPeer()
				.GetTransaction("3a9643dcf9631384df6cb8c7aec50d782e8da5dfd4b44c22cd1f10c6434ee00c");

			Assert.IsNotNull(trans);
		}

		[TestMethod]
		public void GetUnconfirmedTransactionsTest()
		{
			var trans = ArkNetwork.Mainnet.GetRandomPeer().GetTransactions(true);
			Assert.IsTrue(trans.Count >= 0);
		}

		[TestMethod]
		public void GetUnconfirmedTransactionFalseIDTest()
		{
			var trans = ArkNetwork.Mainnet.GetRandomPeer().GetTransaction("WrongID", true);

			Assert.AreEqual(trans.id, "Transaction not found");
		}

		[TestMethod]
		public void AllDelegatesTest()
		{
			var dele = ArkNetwork.Mainnet.GetRandomPeer().GetDelegates();
			Assert.IsTrue(dele.Count > 0);

			var dele1 = ArkNetwork.Mainnet.GetRandomPeer().GetDelegatebyUsername(dele[0].username);
			Assert.IsNotNull(dele1);

			var dele22 = ArkNetwork.Mainnet.GetRandomPeer().GetDelegatebyPubKey(dele[0].publicKey);
			Assert.IsNotNull(dele22);

			var dele33 = ArkNetwork.Mainnet.GetRandomPeer().GetDelegatebyAddress(dele[0].address);
			Assert.IsNotNull(dele33);

			var voters = ArkNetwork.Mainnet.GetRandomPeer().GetDelegateVoters(dele[0].publicKey);
			Assert.IsTrue(voters.Count > 0);
		}

		[TestMethod]
		public void AllAccountsTest()
		{
			var dele = ArkNetwork.Mainnet.GetRandomPeer().GetDelegates();
			Assert.IsTrue(dele.Count > 0);

			var accountTest = ArkNetwork.Mainnet.GetRandomPeer().GetAccountbyAddress(dele[0].address);
			Assert.IsNotNull(accountTest);
		}
	}
}