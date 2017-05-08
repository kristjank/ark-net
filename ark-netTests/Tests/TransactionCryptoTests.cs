using System;
using System.Collections.Generic;
using ArkNet;
using ArkNet.Core;
using ArkNet.Utils.Enum;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBitcoin;
using NBitcoin.DataEncoders;

namespace ArkNetTest.Tests
{
	[TestClass]
	public class TransactionCryptoTests
	{
	    [TestInitialize]
	    public void Init()
	    {
	        ArkNetApi.Instance.Start(NetworkType.MainNet);
	    }

        [TestMethod]
		public void GetKeysTest()
		{
			var key = Crypto.GetKeys("this is a top secret passphrase");

			Assert.AreEqual(key.PubKey.ToString(), "034151a3ec46b5670a682b0a63394f863587d1bc97483b1b6c70eb58e7f0aed192");
		}


		[TestMethod]
		public void GetAddressTest()
		{
			var a1 = Crypto.GetAddress(Crypto.GetKeys("this is a top secret passphrase"),ArkNetApi.Instance.NetworkSettings.PubKeyHash);
			var a2 = "AGeYmgbg2LgGxRW2vNNJvQ88PknEJsYizC";

			Assert.AreEqual(a2, a1);
		}

		[TestMethod]
		public void CreateTransactionPassPhraseVerifyTest()
		{
			var tx = TransactionApi.CreateTransaction("AXoXnFi4z1Z6aFvjEYkDVCtBGW2PaRiM25",
				133380000000,
				"This is first transaction from ARK-NET",
				"this is a top secret passphrase");


			Assert.IsTrue(Crypto.Verify(tx));
		}

		[TestMethod]
		public void JSONSerDeSerTest()
		{
			var tx = TransactionApi.CreateTransaction("AXoXnFi4z1Z6aFvjEYkDVCtBGW2PaRiM25",
				133380000000,
				"This is first transaction from ARK-NET",
				"this is a top secret passphrase",
				"this is a top secret second passphrase");
			var json = tx.ToJson();
			Console.WriteLine(json);

			var tx2 = TransactionApi.FromJson(json);

			Assert.AreEqual(json, tx2.ToJson());
		}

		[TestMethod]
		public void JSONSerDeSerNegTest()
		{
			var tx = TransactionApi.CreateTransaction("AXoXnFi4z1Z6aFvjEYkDVCtBGW2PaRiM25",
				133380000000,
				"This is first transaction from ARK-NET",
				"this is a top secret passphrase",
				"this is a top secret second passphrase");
			var json = tx.ToJson();
			Console.WriteLine(json);

			var tx2 = TransactionApi.FromJson(json);
			tx2.SignSignature = "Change";

			Assert.AreNotEqual(json, tx2.ToJson());
		}

		[TestMethod]
		public void CreateTransaction2ndPassPhraseandVerifyTest()
		{
			var tx = TransactionApi.CreateTransaction("AXoXnFi4z1Z6aFvjEYkDVCtBGW2PaRiM25",
				133380000000,
				"This is first transaction from ARK-NET",
				"this is a top secret passphrase",
				"this is a top secret second passphrase");

			var secondPublicKeyHex = Crypto.GetKeys("this is a top secret second passphrase").PubKey.ToBytes();

			var secondPublicKeyHexStr = Encoders.Hex.EncodeData(secondPublicKeyHex);

			Assert.IsTrue(Crypto.Verify(tx));
			Assert.IsTrue(Crypto.SecondVerify(tx, secondPublicKeyHexStr));
		}

		[TestMethod]
		public void CreateTransactionAmountChangeTest()
		{
			var tx = TransactionApi.CreateTransaction("AXoXnFi4z1Z6aFvjEYkDVCtBGW2PaRiM25",
				133380000000,
				"This is first transaction from ARK-NET",
				"this is a top secret passphrase");
			var json = tx.ToJson();

			tx.Amount = 10100000000000000;

			Assert.IsFalse(Crypto.Verify(tx));
			Assert.AreNotEqual(json, tx.ToJson());
		}


		[TestMethod]
		public void CreateTransactionFeeChangeTest()
		{
			var tx = TransactionApi.CreateTransaction("AXoXnFi4z1Z6aFvjEYkDVCtBGW2PaRiM25",
				133380000000,
				"This is first transaction from ARK-NET",
				"this is a top secret passphrase");
			var json = tx.ToJson();

			tx.Fee = 11;

			Assert.IsFalse(Crypto.Verify(tx));
			Assert.AreNotEqual(json, tx.ToJson());
		}


		[TestMethod]
		public void CreateTransactionRecepientChangeTest()
		{
			var tx = TransactionApi.CreateTransaction("AXoXnFi4z1Z6aFvjEYkDVCtBGW2PaRiM25",
				133380000000,
				"This is first transaction from ARK-NET",
				"this is a top secret passphrase");
			var json = tx.ToJson();

			tx.RecipientId = "AavdJLxqBnWqaFXWm2xNirNArJNUmyUpup";

			Assert.IsFalse(Crypto.Verify(tx));
			Assert.AreNotEqual(json, tx.ToJson());
		}

		[TestMethod]
		public void CreateDelegateTest()
		{
			var tx = TransactionApi.CreateDelegate("polpolo", "this is a top secret passphrase");
			var json = tx.ToJson();

			Assert.IsTrue(Crypto.Verify(tx));
			Assert.AreEqual(json, tx.ToJson());
		}

	    [TestMethod]
	    public void CreateVoteSignTest()
	    {
	        List<string> votes = new List<string> { "+034151a3ec46b5670a682b0a63394f863587d1bc97483b1b6c70eb58e7f0aed192" };


	        var tx = TransactionApi.CreateVote(votes, "ski rose knock live elder parade dose device fetch betray loan holiday");

	        var json = tx.ToObject(true);
            Assert.IsTrue(Crypto.Verify(tx));
	        Assert.AreEqual(json, tx.ToObject(true));
	    }
    }
}