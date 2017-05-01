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
			var peers = NetworkApi.Mainnet.GetRandomPeer().GetPeers();


		    var peer = peers.Where(x => x.Status.Equals("OK")).FirstOrDefault();
//		    var peer = peersOk.;

            Assert.IsNotNull(peer);

		}

		[TestMethod]
		public void GetTransactionsTest()
		{
			var trans = NetworkApi.Mainnet.GetRandomPeer().GetTransactions();
			Assert.IsTrue(trans.Count > 0);
		}

		[TestMethod]
		public void GetPeerStatusTest()
		{
			var peerStat = NetworkApi.Mainnet.GetRandomPeer().GetPeerStatus();

			Assert.IsNotNull(peerStat);
		}

		[TestMethod]
		public void GetTransactionTest()
		{
			var trans = NetworkApi.Mainnet.GetRandomPeer()
				.GetTransaction("3a9643dcf9631384df6cb8c7aec50d782e8da5dfd4b44c22cd1f10c6434ee00c");

			Assert.IsNotNull(trans);
		}

		[TestMethod]
		public void GetUnconfirmedTransactionsTest()
		{
			var trans = NetworkApi.Mainnet.GetRandomPeer().GetTransactions(true);
			Assert.IsTrue(trans.Count >= 0);
		}

		[TestMethod]
		public void GetUnconfirmedTransactionFalseIDTest()
		{
			var trans = NetworkApi.Mainnet.GetRandomPeer().GetTransaction("WrongID", true);

			Assert.AreEqual(trans.Id, "Transaction not found");
		}

		[TestMethod]
		public void AllDelegatesTest()
		{
			var dele = NetworkApi.Mainnet.GetRandomPeer().GetDelegates().FirstOrDefault();
			Assert.IsNotNull(dele);

			var dele1 = NetworkApi.Mainnet.GetRandomPeer().GetDelegatebyUsername(dele.Username);
			Assert.IsNotNull(dele1);

			var dele22 = NetworkApi.Mainnet.GetRandomPeer().GetDelegatebyPubKey(dele.PublicKey);
			Assert.IsNotNull(dele22);

			var dele33 = NetworkApi.Mainnet.GetRandomPeer().GetDelegatebyAddress(dele.Address);
			Assert.IsNotNull(dele33);

			var voters = NetworkApi.Mainnet.GetRandomPeer().GetDelegateVoters(dele.PublicKey);
			Assert.IsTrue(voters.Count > 0);
		}

		[TestMethod]
		public void AllAccountsTest()
		{
			var dele = NetworkApi.Mainnet.GetRandomPeer().GetDelegates().FirstOrDefault();
			Assert.IsNotNull(dele);

			var accountTest = NetworkApi.Mainnet.GetRandomPeer().GetAccountbyAddress(dele.Address);
			Assert.IsNotNull(accountTest);
		}
	}
}