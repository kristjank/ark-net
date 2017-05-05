using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArkNet.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArkNet.Model;
using ArkNet.Service;
using Newtonsoft.Json;

namespace ArkNet.Controller.Tests
{
    [TestClass()]
    public class AccountControllerTests
    {
        [TestMethod()]
        public void AccountControllerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AskRemoteSignatureTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SendMultisignArkTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void OpenAccountTest()
        {
            var accCtnrl = new AccountController();
            accCtnrl.OpenAccount("this is a top secret passphrase");

            Assert.AreEqual(accCtnrl.OpenAccount("this is a top secret passphrase").Address, "AGeYmgbg2LgGxRW2vNNJvQ88PknEJsYizC");
            Assert.AreEqual(accCtnrl.OpenAccount("this is a top secret passphrase").PublicKey, "034151a3ec46b5670a682b0a63394f863587d1bc97483b1b6c70eb58e7f0aed192");
        }

        [TestMethod()]
        public void SendArkTest()
        {
            var accCtnrl = new AccountController();
            var result = accCtnrl.SendArk(1234, "ASJBHz4JfWVUGDyN61hMMnW1Y4ZCTBHL1K", "Akr.Net test trans from Account",
                "ski rose knock live elder parade dose device fetch betray loan holiday");
           
            Assert.IsTrue(result.status);
        }

        [TestMethod()]
        public void VoteForDelegateTest()
        {
            var dele = DelegateService.GetByUsername("arkpool");

            List<string> votes = new List<string>();
            votes.Add("+" + dele.PublicKey);

            var a2 = JsonConvert.SerializeObject(votes);

            var accCtnrl = new AccountController();
            var result = accCtnrl.VoteForDelegate( votes, "ski rose knock live elder parade dose device fetch betray loan holiday");


            Assert.Fail();
        }

        [TestMethod()]
        public void RegisterAsDelegateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoteSignTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RegisterSecondSignatureTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetVoterContributionTest()
        {
            Assert.Fail();
        }
    }
}