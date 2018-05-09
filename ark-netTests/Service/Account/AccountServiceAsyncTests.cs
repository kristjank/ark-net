using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArkNet.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArkNet.Utils.Enum;

namespace ArkNet.Service.Account.Tests
{
    [TestClass()]
    public class AccountServiceAsyncTests : AccountServiceTestsBase
    {
        [TestInitialize]
        public async Task Init()
        {
            await base.InitializeAccountServiceAsyncTest();
        }

        [TestMethod()]
        public async Task GetByAddressAsyncTest()
        {
            var account = await ArkNetApi.AccountService.GetByAddressAsync(_address);

            GetByAddressResultTest(account);
        }

        [TestMethod()]
        public async Task GetByAddressErrorAsyncTest()
        {
            var account = await ArkNetApi.AccountService.GetByAddressAsync("BadAddress");

            GetByAddressErrorResultTest(account);
        }

        [TestMethod()]
        public async Task GetBalanceAsyncTest()
        {
            var res = await ArkNetApi.AccountService.GetBalanceAsync(_address);

            GetBalanceResultTest(res);
        }

        [TestMethod()]
        public async Task GetBalanceErrorAsyncTest()
        {
            var res = await ArkNetApi.AccountService.GetBalanceAsync("BadAddress");

            GetBalanceErrorResultTest(res);
        }

        [TestMethod()]
        public async Task GetDelegatesAsyncTest()
        {
            var delegates = await ArkNetApi.AccountService.GetDelegatesAsync(_address);

            GetDelegatesResultTest(delegates);
        }

        [TestMethod()]
        public async Task GetDelegatesErrorAsyncTest()
        {
            var delegates = await ArkNetApi.AccountService.GetDelegatesAsync("BadAddress");

            GetDelegatesErrorResultTest(delegates);
        }

        [TestMethod()]
        public async Task GetTopAsyncTest()
        {
            var top = await ArkNetApi.AccountService.GetTopAsync(null, null);

            GetTopResultTest(top);
        }

        [TestMethod()]
        public async Task GetTopLimitAsyncTest()
        {
            var top = await ArkNetApi.AccountService.GetTopAsync(10, null);

            GetTopLimitResultTest(top);
        }

        [TestMethod()]
        public async Task GetTopRecordsToSkipAsyncTest()
        {
            var top = await ArkNetApi.AccountService.GetTopAsync(null, 50);

            GetTopRecordsToSkipResultTest(top);
        }

        [TestMethod()]
        public async Task GetTopLimitAndRecordsToSkipAsyncTest()
        {
            var top = await ArkNetApi.AccountService.GetTopAsync(10, 50);

            GetTopLimitAndRecordsToSkipResultTest(top);
        }

        [TestMethod()]
        public async Task GetTopLimitErrorAsyncTest()
        {
            var top = await ArkNetApi.AccountService.GetTopAsync(1000, null);

            GetTopLimitErrorResultTest(top);
        }
    }
}