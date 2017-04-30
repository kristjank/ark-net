using System.Linq;
using ArkNet.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ark_netTests.Tests
{
	[TestClass]
	public class ModelApiVOTests
	{
		[TestMethod]
		public void GetPeersTest()
		{
			var peers = Network.Mainnet.GetRandomPeer().GetPeers();


			var peersOk = peers.Where(x => x.status.Equals("OK"));
			var a = peersOk.Count();

			Assert.IsTrue(peers.Count > 0);
		}

		[TestMethod]
		public void GetTransactionsTest()
		{
			var trans = Network.Mainnet.GetRandomPeer().GetTransactions();
			Assert.IsTrue(trans.Count > 0);
		}

		[TestMethod]
		public void GetPeerStatusTest()
		{
			var peerStat = Network.Mainnet.GetRandomPeer().GetPeerStatus();

			Assert.IsNotNull(peerStat);
		}

		[TestMethod]
		public void GetTransactionTest()
		{
			var trans = Network.Mainnet.GetRandomPeer()
				.GetTransaction("3a9643dcf9631384df6cb8c7aec50d782e8da5dfd4b44c22cd1f10c6434ee00c");

			Assert.IsNotNull(trans);
		}

		[TestMethod]
		public void GetUnconfirmedTransactionsTest()
		{
			var trans = Network.Mainnet.GetRandomPeer().GetTransactions(true);
			Assert.IsTrue(trans.Count >= 0);
		}

		[TestMethod]
		public void GetUnconfirmedTransactionFalseIDTest()
		{
			var trans = Network.Mainnet.GetRandomPeer().GetTransaction("WrongID", true);

			Assert.AreEqual(trans.id, "Transaction not found");
		}

		[TestMethod]
		public void AllDelegatesTest()
		{
			var dele = Network.Mainnet.GetRandomPeer().GetDelegates();
			Assert.IsTrue(dele.Count > 0);

			var dele1 = Network.Mainnet.GetRandomPeer().GetDelegatebyUsername(dele[0].username);
			Assert.IsNotNull(dele1);

			var dele22 = Network.Mainnet.GetRandomPeer().GetDelegatebyPubKey(dele[0].publicKey);
			Assert.IsNotNull(dele22);

			var dele33 = Network.Mainnet.GetRandomPeer().GetDelegatebyAddress(dele[0].address);
			Assert.IsNotNull(dele33);

			var voters = Network.Mainnet.GetRandomPeer().GetDelegateVoters(dele[0].publicKey);
			Assert.IsTrue(voters.Count > 0);
		}

		[TestMethod]
		public void AllAccountsTest()
		{
			var dele = Network.Mainnet.GetRandomPeer().GetDelegates();
			Assert.IsTrue(dele.Count > 0);

			var accountTest = Network.Mainnet.GetRandomPeer().GetAccountbyAddress(dele[0].address);
			Assert.IsNotNull(accountTest);
		}
	}
}