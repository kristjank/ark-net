using System.IO;
using ArkNet;
using ArkNet.Core;
using ArkNet.Service;
using ArkNet.Utils;
using ArkNet.Utils.Enum;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArkNet.Tests;
using System.Threading.Tasks;
using System.Linq;

namespace ArkNetTest.Tests
{
    [TestClass]
	public class NetworkTests : TestsBase
	{
        /* TEST MAIN WALLET
	     Adress: "AQLUKKKyKq5wZX7rCh4HJ4YFQ8bpTpPJgK"
	     pass: ski rose knock live elder parade dose device fetch betray loan holiday
	     */

        private string _address = "AQLUKKKyKq5wZX7rCh4HJ4YFQ8bpTpPJgK";
        private string _passPhrase = "ski rose knock live elder parade dose device fetch betray loan holiday";
        private string _noBalanceAddress = "AXoXnFi4z1Z6aFvjEYkDVCtBGW2PaRiM25";
        private string _noBalanceAddressPassPhrase = "ski rose knock live elder parade dose device fetch betray loan holiday";

        [TestInitialize]
	    public void Init()
	    {
            base.Initialize();

            if (USE_DEV_NET)
            {
                _address = "DRAJSs7GFq8iH1UGPAm8jVW9CgU1qwhkit";
                _noBalanceAddress = "D5MYnTQCSy7ycYWuZv1ogWfBxTtQ1RNx6y";
                _passPhrase = "sorry mandate shadow civil girl vote fragile senior also clip abandon milk";
                _noBalanceAddressPassPhrase = "donkey click monster month diamond car actor news forward course ask blue";
            }
	    }

        [TestMethod]
		public void PostTransactionNoBalanceTest()
		{
			var tx = ArkNetApi.TransactionApi.CreateTransaction(_noBalanceAddress,
				133380000000,
				"This is first transaction from ARK-NET",
                _noBalanceAddressPassPhrase);

			var result = ArkNetApi.TransactionService.PostTransaction(tx);

			Assert.AreEqual(result.Error, string.Format("Account does not have enough ARK: {0} balance: 0", _noBalanceAddress));
		}


		[TestMethod]
		public void TransactionSerializeTest()
		{
			var tx = ArkNetApi.TransactionApi.CreateTransaction(_address,
				1,
				"This is first transaction from ARK-NET 22",
                _passPhrase);

			tx.Timestamp = 100;
			File.WriteAllText(@"C:\temp\txNew.json", tx.SerializeObject2JSon());
			File.WriteAllText(@"C:\temp\txNew.xml", tx.SerializeObject2Xml());

			Assert.IsTrue(1 == 1);
		}


		[TestMethod]
		public void PostTransactionTransferTest()
		{
			var tx = ArkNetApi.TransactionApi.CreateTransaction(_address,
				1,
				"This is first transaction from ARK-NET 22",
				_passPhrase);

			var result = ArkNetApi.TransactionService.PostTransaction(tx);

			Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.TransactionIds);
            Assert.IsTrue(result.TransactionIds.Count > 0);
        }

		[TestMethod]
		public void MultiplePostTransactionSuccessTest()
		{
			var tx = ArkNetApi.TransactionApi.CreateTransaction(_address,
				1,
				"This is first Multi transaction from ARK-NET",
                _passPhrase);

			var res = ArkNetApi.TransactionService.MultipleBroadCast(tx);

			Assert.IsTrue(res.Where(x => x.Success).Count() > 0);
		}

        [TestMethod]
        public async Task MultiplePostTransactionAsyncSuccessTest()
        {
            var tx = ArkNetApi.TransactionApi.CreateTransaction(_address,
                1,
                "This is first Multi transaction from ARK-NET",
                _passPhrase);

            var res = await ArkNetApi.TransactionService.MultipleBroadCastAsync(tx);

            Assert.IsTrue(res.Where(x => x.Success).Count() > 0);
        }
    }
}