﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArkNet.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArkNet.Model;
using ArkNet.Service;
using ArkNet.Utils.Enum;
using Newtonsoft.Json;
using ArkNet.Tests;

namespace ArkNet.Controller.Tests
{
    [TestClass()]
    public class AccountControllerTests : TestsBase
    {
        private string _address = "AQLUKKKyKq5wZX7rCh4HJ4YFQ8bpTpPJgK";
        private string _pubKey = "02a3438fa0da25ebfb2c3df4875e30ff6d687b94adb9f2326ed988c101bdeeb973";
        private string _passPhrase = "ski rose knock live elder parade dose device fetch betray loan holiday";
        private string _delegateName = "arkpool";

        [TestInitialize]
        public void Init()
        {
            base.Initialize();

            if (USE_DEV_NET)
            {
                _address = "DRAJSs7GFq8iH1UGPAm8jVW9CgU1qwhkit";
                _pubKey = "024b3c846cee903a60476e6912cdde21722b9d8e74a94ac88ac870e0e92a5b12a3";
                _passPhrase = "sorry mandate shadow civil girl vote fragile senior also clip abandon milk";
                _delegateName = "d_chris";
            }
        }

        //[TestMethod()]
        //public void AccountControllerTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void AskRemoteSignatureTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void SendMultisignArkTest()
        //{
        //    Assert.Fail();
        //}

        [TestMethod()]
        public void CreateAccountTest()
        {
            var accCtnrl = new AccountController(_passPhrase);

            Assert.AreEqual(accCtnrl.GetArkAccount().Address, _address);
            Assert.AreEqual(accCtnrl.GetArkAccount().PublicKey, _pubKey);
        }

        [TestMethod()]
        public void SendArkTest()
        {
            var accCtnrl = new AccountController(_passPhrase);
            var result = accCtnrl.SendArk(1, _address, "Akr.Net test trans from Account");

            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.TransactionIds);
            Assert.IsTrue(result.TransactionIds.Count > 0);
        }

        [TestMethod()]
        public void SendArkUsingMultiBroadCastTest()
        {
            var accCtnrl = new AccountController(_passPhrase);
            var result = accCtnrl.SendArkUsingMultiBroadCast(1, _address, "Akr.Net test multi-trans from Account");

            Assert.IsTrue(result > 0);
        }

        [TestMethod()]
        public async Task SendArkUsingMultiBroadCastAsyncTest()
        {
            var accCtnrl = new AccountController(_passPhrase);
            var result = await accCtnrl.SendArkUsingMultiBroadCastAsync(1, _address, "Akr.Net test multi-trans from Account");

            Assert.IsTrue(result > 0);
        }

        [TestMethod()]
        public void VoteForDelegateTest()
        {
            var dele = DelegateService.GetByUsername(_delegateName);

            List<string> votes = new List<string>();
            votes.Add("+" + dele.Delegate.PublicKey);

            var a2 = JsonConvert.SerializeObject(votes);

            var accCtnrl = new AccountController(_passPhrase);
            var result = accCtnrl.VoteForDelegate(votes);

            Assert.IsTrue(result.Success || (result.Success == false && result.TransactionIds == null && result.Error == "Failed to add vote, account has already voted for this delegate"));
        }

        //[TestMethod()]
        //public void RegisterAsDelegateTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void RemoteSignTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void RegisterSecondSignatureTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void GetVoterContributionTest()
        //{
        //    Assert.Fail();
        //}
    }
}