using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArkNet.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArkNet.Utils.Enum;
using ArkNet.Messages.BaseMessages;

namespace ArkNet.Service.Delegate.Tests
{
    [TestClass()]
    public class DelegateServiceAsyncTests : DelegateServiceTestsBase
    {
        [TestInitialize]
        public async Task Init()
        {
            await base.InitializeDelegateServiceAsyncTest();
        }

        [TestMethod()]
        public async Task GetAllAsyncTest()
        {
            var delegates = await ArkNetApi.DelegateService.GetAllAsync();

            GetAllResultTest(delegates);
        }

        [TestMethod()]
        public async Task GetDelegatesAsyncTest()
        {
            var delegates = await ArkNetApi.DelegateService.GetDelegatesAsync(new ArkBaseRequest { OrderBy = "vote" });

            GetAllResultTest(delegates);
        }

        [TestMethod()]
        public async Task GetByUsernameAsyncTest()
        {
            var dele = await ArkNetApi.DelegateService.GetByUsernameAsync(_userName);

            GetByUsernameResultTest(dele);
        }

        [TestMethod()]
        public async Task GetByUsernameErrorAsyncTest()
        {
            var dele = await ArkNetApi.DelegateService.GetByUsernameAsync("NonExistingPool");

            GetByUsernameErrorResultTest(dele);
        }

        [TestMethod()]
        public async Task GetByPubKeyAsyncTest()
        {
            var dele2 = await ArkNetApi.DelegateService.GetByPubKeyAsync(_pubKey);

            GetByPubKeyResultTest(dele2);
        }

        [TestMethod()]
        public async Task GetByPubKeyErrorAsyncTest()
        {
            var dele = await ArkNetApi.DelegateService.GetByPubKeyAsync("ErrorKey");

            GetByPubKeyErrorResultTest(dele);
        }

        [TestMethod()]
        public async Task GetVotersAsyncTest()
        {
            var dele = await ArkNetApi.DelegateService.GetVotersAsync(_pubKey);

            GetVotersResultTest(dele);
        }

        [TestMethod()]
        public async Task GetVotersErrorAsyncTest()
        {
            var dele = await ArkNetApi.DelegateService.GetVotersAsync("ErrorKey");

            GetVotersErrorResultTest(dele);
        }

        [TestMethod()]
        public async Task GetFeeAsyncTest()
        {
            var fee = await ArkNetApi.DelegateService.GetFeeAsync();

            GetFeeResultTest(fee);
        }

        [TestMethod()]
        public async Task GetForgedByAccountAsyncTest()
        {
            var forgedByAccount = await ArkNetApi.DelegateService.GetForgedByAccountAsync(_pubKey);

            GetForgedByAccountResultTest(forgedByAccount);
        }

        [TestMethod()]
        public async Task GetForgedByAccountErrorAsyncTest()
        {
            var forgedByAccount = await ArkNetApi.DelegateService.GetForgedByAccountAsync("ErrorKey");

            GetForgedByAccountErrorResultTest(forgedByAccount);
        }

        [TestMethod()]
        public async Task GetNextForgersAsyncTest()
        {
            var nextForgers = await ArkNetApi.DelegateService.GetNextForgersAsync();

            GetNextForgersResultTest(nextForgers);
        }

        [TestMethod()]
        public async Task GetTotalVoteArkAsyncTest()
        {
            var totalVoteArk = await ArkNetApi.DelegateService.GetTotalVoteArkAsync(_pubKey);

            GetTotalVoteArkResultTest(totalVoteArk);
        }

        [TestMethod()]
        public async Task GetTotalVoteArkErrorAsyncTest()
        {
            var totalVoteArk = await ArkNetApi.DelegateService.GetTotalVoteArkAsync("ErrorKey");

            GetTotalVoteArkErrorResultTest(totalVoteArk);
        }
    }
}