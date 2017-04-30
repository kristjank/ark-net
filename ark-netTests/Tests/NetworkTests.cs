using System.IO;
using ArkNet.Core;
using ArkNet.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArkNetTest.Tests
{
	[TestClass]
	public class NetworkTests
	{
		/* TEST MAIN WALLET
	     Adress: "AQLUKKKyKq5wZX7rCh4HJ4YFQ8bpTpPJgK"
	     pass: ski rose knock live elder parade dose device fetch betray loan holiday
	     */

		[TestMethod]
		public void PostTransactionNoBalanceTest()
		{
			var tx = Transaction.CreateTransaction("AXoXnFi4z1Z6aFvjEYkDVCtBGW2PaRiM25",
				133380000000,
				"This is first transaction from ARK-NET",
				"this is a top secret passphrase");

			var peer = Network.Mainnet.GetRandomPeer();


			var result = peer.PostTransaction(tx);

			Assert.AreEqual(result.Item3, "Account does not have enough ARK: AGeYmgbg2LgGxRW2vNNJvQ88PknEJsYizC balance: 0");
		}


		[TestMethod]
		public void TransactionSerializeTest()
		{
			var tx = Transaction.CreateTransaction("ASJBHz4JfWVUGDyN61hMMnW1Y4ZCTBHL1K",
				1000,
				"This is first transaction from ARK-NET 22",
				"ski rose knock live elder parade dose device fetch betray loan holiday");

			tx.Timestamp = 100;
			File.WriteAllText(@"C:\temp\txNew.json", tx.SerializeObject2JSon());
			File.WriteAllText(@"C:\temp\txNew.xml", tx.SerializeObject2Xml());


			Assert.IsTrue(1 == 1);
		}


		[TestMethod]
		public void PostTransactionTransferTest()
		{
			var tx = Transaction.CreateTransaction("ASJBHz4JfWVUGDyN61hMMnW1Y4ZCTBHL1K",
				1000,
				"This is first transaction from ARK-NET 22",
				"ski rose knock live elder parade dose device fetch betray loan holiday");

			var peer = Network.Mainnet.GetRandomPeer();

			var result = peer.PostTransaction(tx);
			Assert.IsTrue(result.Item1);
		}

		[TestMethod]
		public void MultiplePostTransactionSuccessTest()
		{
			var tx = Transaction.CreateTransaction("ASJBHz4JfWVUGDyN61hMMnW1Y4ZCTBHL1K",
				1000,
				"This is first Multi transaction from ARK-NET",
				"ski rose knock live elder parade dose device fetch betray loan holiday");

			var res = Network.Mainnet.MultipleBroadCast(tx);
			Assert.IsTrue(res > 0);
		}
	}
}