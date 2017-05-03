using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArkNet.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArkNet.Model;

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
            var accCtnrl = new AccountController(new Account());
            accCtnrl.OpenAccount("this is a top secret passphrase");

            Assert.Fail();
        }

        [TestMethod()]
        public void SendArkTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void VoteForDelegateTest()
        {
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