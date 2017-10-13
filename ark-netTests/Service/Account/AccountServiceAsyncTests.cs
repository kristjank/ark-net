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
            await ArkNetApi.Instance.Start(NetworkType.MainNet);
        }

        [TestMethod()]
        public async Task GetByAddressAsyncTest()
        {
            var account = await AccountService.GetByAddressAsync("AQLUKKKyKq5wZX7rCh4HJ4YFQ8bpTpPJgK");

            GetByAddressResultTest(account);
        }

        [TestMethod()]
        public async Task GetByAddressErrorAsyncTest()
        {
            var account = await AccountService.GetByAddressAsync("BadAddress");

            GetByAddressErrorResultTest(account);
        }

        [TestMethod()]
        public async Task GetBalanceAsyncTest()
        {
            var res = await AccountService.GetBalanceAsync("AQLUKKKyKq5wZX7rCh4HJ4YFQ8bpTpPJgK");

            GetBalanceResultTest(res);
        }

        [TestMethod()]
        public async Task GetBalanceErrorAsyncTest()
        {
            var res = await AccountService.GetBalanceAsync("BadAddress");

            GetBalanceErrorResultTest(res);
        }

        [TestMethod()]
        public async Task GetDelegatesAsyncTest()
        {
            var delegates = await AccountService.GetDelegatesAsync("AQLUKKKyKq5wZX7rCh4HJ4YFQ8bpTpPJgK");

            GetDelegatesResultTest(delegates);
        }

        [TestMethod()]
        public async Task GetDelegatesErrorAsyncTest()
        {
            var delegates = await AccountService.GetDelegatesAsync("BadAddress");

            GetDelegatesErrorResultTest(delegates);
        }

        [TestMethod()]
        public async Task GetTopAsyncTest()
        {
            var top = await AccountService.GetTopAsync(null, null);

            GetTopResultTest(top);
        }

        [TestMethod()]
        public async Task GetTopLimitAsyncTest()
        {
            var top = await AccountService.GetTopAsync(10, null);

            GetTopLimitResultTest(top);
        }

        [TestMethod()]
        public async Task GetTopRecordsToSkipAsyncTest()
        {
            var top = await AccountService.GetTopAsync(null, 50);

            GetTopRecordsToSkipResultTest(top);
        }

        [TestMethod()]
        public async Task GetTopLimitAndRecordsToSkipAsyncTest()
        {
            var top = await AccountService.GetTopAsync(10, 50);

            GetTopLimitAndRecordsToSkipResultTest(top);
        }

        [TestMethod()]
        public async Task GetTopLimitErrorAsyncTest()
        {
            var top = await AccountService.GetTopAsync(1000, null);

            GetTopLimitErrorResultTest(top);
        }
    }
}