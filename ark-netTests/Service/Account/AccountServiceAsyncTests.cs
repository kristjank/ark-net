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
            var account = await AccountService.GetByAddressAsync(_address);

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
            var res = await AccountService.GetBalanceAsync(_address);

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
            var delegates = await AccountService.GetDelegatesAsync(_address);

            GetDelegatesResultTest(delegates);
        }

        [TestMethod()]
        public async Task GetDelegatesErrorAsyncTest()
        {
            var delegates = await AccountService.GetDelegatesAsync("BadAddress");

            GetDelegatesErrorResultTest(delegates);
        }
    }
}