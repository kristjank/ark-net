using Microsoft.VisualStudio.TestTools.UnitTesting;
using io.ark.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;
using io.ark.model;
<<<<<<< HEAD
using System.IO;
using io.io.ark.utils;
=======
>>>>>>> parent of 7d4c545... transaction.log.obj

namespace io.ark.core.Tests
{
    [TestClass()]
    public class NetworkTests
    {
        /* TEST MAIN WALLET
         Adress: "AQLUKKKyKq5wZX7rCh4HJ4YFQ8bpTpPJgK"
         pass: ski rose knock live elder parade dose device fetch betray loan holiday
         */

        [TestMethod()]
        public void PostTransactionNoBalanceTest()
        {
            Transaction tx = Transaction.CreateTransaction("AXoXnFi4z1Z6aFvjEYkDVCtBGW2PaRiM25",
                                                              133380000000,
                                                            "This is first transaction from ARK-NET",
                                                              "this is a top secret passphrase");

            //Network.Mainnet.WarmUp();
            Peer peer = Network.Mainnet.GetRandomPeer();


            string result = peer.PostTransaction(tx);

            Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(result);

            Assert.AreEqual(jObject["error"], "Account does not have enough ARK: AGeYmgbg2LgGxRW2vNNJvQ88PknEJsYizC balance: 0");
        }


        [TestMethod()]
        public void TransactionSerializeTest()
        {
            Transaction tx = Transaction.CreateTransaction("ASJBHz4JfWVUGDyN61hMMnW1Y4ZCTBHL1K",
                1000,
                "This is first transaction from ARK-NET 22",
                "ski rose knock live elder parade dose device fetch betray loan holiday");


            File.WriteAllText(@"C:\temp\txOK.json", ArkUtils.SerializeObject2JSon(tx));
            File.WriteAllText(@"C:\temp\txOK.xml", ArkUtils.SerializeObject2Xml(tx));
            
            
            Assert.IsTrue(1==1);
        }


        [TestMethod()]
        public void PostTransactionTransferSuccessTest()
        {
            Transaction tx = Transaction.CreateTransaction("ASJBHz4JfWVUGDyN61hMMnW1Y4ZCTBHL1K",
                                                           1000,
                                                           "This is first transaction from ARK-NET 22",
                                                           "ski rose knock live elder parade dose device fetch betray loan holiday");

<<<<<<< HEAD

=======
>>>>>>> parent of 7d4c545... transaction.log.obj
            //Network.Mainnet.WarmUp();
            Peer peer = Network.Mainnet.GetRandomPeer();


            string result = peer.PostTransaction(tx);

            Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(result);

            Assert.IsTrue(Convert.ToBoolean(jObject["success"]));
        }

        [TestMethod()]
        public void MultiplePostTransactionSuccessTest()
        {
            Transaction tx = Transaction.CreateTransaction("ASJBHz4JfWVUGDyN61hMMnW1Y4ZCTBHL1K",
                                                           1000,
                                                           "This is first Multi transaction from ARK-NET",
                                                           "ski rose knock live elder parade dose device fetch betray loan holiday");

            //Network.Mainnet.WarmUp();
            int res = Network.Mainnet.MultipleBroadCast(tx);
            Assert.IsTrue(res > 0);

        }        
    }
}