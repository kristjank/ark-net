using Microsoft.VisualStudio.TestTools.UnitTesting;
using io.ark.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using org.bitcoinj.core;
using com.google.common.io;

namespace io.ark.core.Tests
{
    [TestClass()]
    public class TransactionTests
    {
        [TestMethod()]
        public void CreateTransactionPassPhraseVerifyTest()
        {
            Transaction tx = Transaction.CreateTransaction("AXoXnFi4z1Z6aFvjEYkDVCtBGW2PaRiM25",
                                                            133380000000,
                                                            "This is first transaction from ARK-NET",
                                                            "this is a top secret passphrase");



            Assert.IsTrue(Crypto.Verify(tx));
        }

        [TestMethod()]
        public void JSONSerDeSerTest()
        {
            Transaction tx = Transaction.CreateTransaction("AXoXnFi4z1Z6aFvjEYkDVCtBGW2PaRiM25",
                                                            133380000000,
                                                            "This is first transaction from ARK-NET",
                                                            "this is a top secret passphrase",
                                                            "this is a top secret second passphrase");
            String json = tx.ToJson();
            Console.WriteLine(json);

            Transaction tx2 = Transaction.FromJson(json);

            Assert.AreEqual(json, tx2.ToJson());
        }

        [TestMethod()]
        public void JSONSerDeSerNegTest()
        {
            Transaction tx = Transaction.CreateTransaction("AXoXnFi4z1Z6aFvjEYkDVCtBGW2PaRiM25",
                                                            133380000000,
                                                            "This is first transaction from ARK-NET",
                                                            "this is a top secret passphrase",
                                                            "this is a top secret second passphrase");
            String json = tx.ToJson();
            Console.WriteLine(json);

            Transaction tx2 = Transaction.FromJson(json);
            tx2.SignSignature = "Change";

            Assert.AreNotEqual(json, tx2.ToJson());
        }

        [TestMethod()]
        public void CreateTransaction2ndPassPhraseandVerifyTest()
        {
            Transaction tx = Transaction.CreateTransaction("AXoXnFi4z1Z6aFvjEYkDVCtBGW2PaRiM25",
                                                            133380000000,
                                                            "This is first transaction from ARK-NET",
                                                            "this is a top secret passphrase",
                                                            "this is a top secret second passphrase");

            byte[] secondPublicKeyHex = Crypto.GetKeys("this is a top secret second passphrase").getPubKey();

            String secondPublicKeyHexStr = BaseEncoding.base16().lowerCase().encode(secondPublicKeyHex);

            Assert.IsTrue(Crypto.Verify(tx));
            Assert.IsTrue(Crypto.SecondVerify(tx, secondPublicKeyHexStr));
        }

        [TestMethod()]
        public void CreateTransactionAmountChangeTest()
        {
            Transaction tx = Transaction.CreateTransaction("AXoXnFi4z1Z6aFvjEYkDVCtBGW2PaRiM25",
                                                            133380000000,
                                                            "This is first transaction from ARK-NET",
                                                            "this is a top secret passphrase");
            String json = tx.ToJson();

            tx.Amount = 10100000000000000;

            Assert.IsFalse(Crypto.Verify(tx));
            Assert.AreNotEqual(json, tx.ToJson());
        }


        [TestMethod()]
        public void CreateTransactionFeeChangeTest()
        {
            Transaction tx = Transaction.CreateTransaction("AXoXnFi4z1Z6aFvjEYkDVCtBGW2PaRiM25",
                                                            133380000000,
                                                            "This is first transaction from ARK-NET",
                                                            "this is a top secret passphrase");
            String json = tx.ToJson();

            tx.Fee = 11;

            Assert.IsFalse(Crypto.Verify(tx));
            Assert.AreNotEqual(json, tx.ToJson());
        }



        [TestMethod()]
        public void CreateTransactionRecepientChangeTest()
        {
            Transaction tx = Transaction.CreateTransaction("AXoXnFi4z1Z6aFvjEYkDVCtBGW2PaRiM25",
                                                            133380000000,
                                                            "This is first transaction from ARK-NET",
                                                            "this is a top secret passphrase");
            String json = tx.ToJson();

            tx.RecipientId = "AavdJLxqBnWqaFXWm2xNirNArJNUmyUpup";

            Assert.IsFalse(Crypto.Verify(tx));
            Assert.AreNotEqual(json, tx.ToJson());
        }

        [TestMethod()]
        public void CreateDelegateTest()
        {

            Transaction tx = Transaction.CreateDelegate("polpolo","this is a top secret passphrase");
            string json = tx.ToJson();

            Assert.IsTrue(Crypto.Verify(tx));
            Assert.AreEqual(json, tx.ToJson());
        }
    }
}