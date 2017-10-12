using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArkNet.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArkNet.Utils.Enum;

namespace ArkNet.Service.Tests
{
    [TestClass()]
    public class AccountServiceAsyncTests
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

            Assert.IsNotNull(account);
            Assert.IsNotNull(account.Account);
            Assert.IsTrue(account.Success);
            Assert.IsNull(account.Error);
            Assert.AreEqual("AQLUKKKyKq5wZX7rCh4HJ4YFQ8bpTpPJgK", account.Account.Address);
        }

        [TestMethod()]
        public async Task GetByAddressErrorAsyncTest()
        {
            var account = await AccountService.GetByAddressAsync("BadAddress");

            Assert.IsNotNull(account);
            Assert.IsNull(account.Account);
            Assert.IsFalse(account.Success);
            Assert.IsNotNull(account.Error);
        }

        [TestMethod()]
        public async Task GetBalanceAsyncTest()
        {
            var res = await AccountService.GetBalanceAsync("AQLUKKKyKq5wZX7rCh4HJ4YFQ8bpTpPJgK");

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Success);
            Assert.IsNull(res.Error);
        }

        [TestMethod()]
        public async Task GetBalanceErrorAsyncTest()
        {
            var res = await AccountService.GetBalanceAsync("BadAddress");

            Assert.IsNotNull(res);
            Assert.IsFalse(res.Success);
            Assert.IsNotNull(res.Error);
        }

        [TestMethod()]
        public async Task GetDelegatesAsyncTest()
        {
            var delegates = await AccountService.GetDelegatesAsync("AQLUKKKyKq5wZX7rCh4HJ4YFQ8bpTpPJgK");

            Assert.IsNotNull(delegates);
            Assert.IsNotNull(delegates.Delegates);
            Assert.IsTrue(delegates.Success);
            Assert.IsNull(delegates.Error);
        }

        [TestMethod()]
        public async Task GetDelegatesErrorAsyncTest()
        {
            var delegates = await AccountService.GetDelegatesAsync("BadAddress");

            Assert.IsNotNull(delegates);
            Assert.IsNull(delegates.Delegates);
            Assert.IsFalse(delegates.Success);
            Assert.IsNotNull(delegates.Error);
        }

        [TestMethod()]
        public async Task GetTopAsyncTest()
        {
            var top = await AccountService.GetTopAsync(null, null);

            Assert.IsNotNull(top);
            Assert.IsNotNull(top.Accounts);
            Assert.IsTrue(top.Success);
            Assert.IsNull(top.Error);
            Assert.AreEqual(100, top.Accounts.Count);
        }

        [TestMethod()]
        public async Task GetTopLimitAsyncTest()
        {
            var top = await AccountService.GetTopAsync(10, null);

            Assert.IsNotNull(top);
            Assert.IsNotNull(top.Accounts);
            Assert.IsTrue(top.Success);
            Assert.IsNull(top.Error);
            Assert.AreEqual(10, top.Accounts.Count);
        }

        [TestMethod()]
        public async Task GetTopRecordsToSkipAsyncTest()
        {
            var top = await AccountService.GetTopAsync(null, 50);

            Assert.IsNotNull(top);
            Assert.IsNotNull(top.Accounts);
            Assert.IsTrue(top.Success);
            Assert.IsNull(top.Error);
            Assert.AreEqual(100, top.Accounts.Count);
        }

        [TestMethod()]
        public async Task GetTopLimitAndRecordsToSkipAsyncTest()
        {
            var top = await AccountService.GetTopAsync(10, 50);

            Assert.IsNotNull(top);
            Assert.IsNotNull(top.Accounts);
            Assert.IsTrue(top.Success);
            Assert.IsNull(top.Error);
            Assert.AreEqual(10, top.Accounts.Count);
        }

        [TestMethod()]
        public async Task GetTopLimitErrorAsyncTest()
        {
            var top = await AccountService.GetTopAsync(1000, null);

            Assert.IsNotNull(top);
            Assert.IsFalse(top.Success);
            Assert.IsNotNull(top.Error);
        }
    }
}