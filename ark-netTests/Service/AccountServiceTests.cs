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
    public class AccountServiceTests
    {
        [TestInitialize]
        public void Init()
        {
            ArkNetApi.Instance.Start(NetworkType.MainNet).Wait();
        }

        [TestMethod()]
        public void GetByAddressTest()
        {
            var account = AccountService.GetByAddress("AQLUKKKyKq5wZX7rCh4HJ4YFQ8bpTpPJgK");

            Assert.IsNotNull(account);
            Assert.IsTrue(account.Success);
        }

        [TestMethod()]
        public void GetByAddressErrorTest()
        {
            var account = AccountService.GetByAddress("BadAddress");

            Assert.IsFalse(account.Success);
            Assert.IsNotNull(account.Error);
        }

        [TestMethod()]
        public void GetBalanceTest()
        {
            var res = AccountService.GetBalance("AQLUKKKyKq5wZX7rCh4HJ4YFQ8bpTpPJgK");

            Assert.IsTrue(res.Success);
        }

        [TestMethod()]
        public void GetBalanceErrorTest()
        {
            var res = AccountService.GetBalance("BadAddress");

            Assert.IsFalse(res.Success);
            Assert.IsNotNull(res.Error);
        }

        [TestMethod()]
        public void GetDelegatesTest()
        {
            var delegates = AccountService.GetDelegates("AQLUKKKyKq5wZX7rCh4HJ4YFQ8bpTpPJgK");

            Assert.IsNotNull(delegates);
        }

        [TestMethod()]
        public void GetDelegatesErrorTest()
        {
            var delegates = AccountService.GetDelegates("BadAddress");

            Assert.IsFalse(delegates.Success);
            Assert.IsNotNull(delegates.Error);
        }

        [TestMethod()]
        public void GetTopTest()
        {
            var top = AccountService.GetTop(null, null);

            Assert.IsNotNull(top);
        }

        [TestMethod()]
        public void GetTopLimitTest()
        {
            var top = AccountService.GetTop(10, null);

            Assert.IsNotNull(top);
        }

        [TestMethod()]
        public void GetTopRecordsToSkipTest()
        {
            var top = AccountService.GetTop(null, 50);

            Assert.IsNotNull(top);
        }

        [TestMethod()]
        public void GetTopLimitAndRecordsToSkipTest()
        {
            var top = AccountService.GetTop(10, 50);

            Assert.IsNotNull(top);
        }

        [TestMethod()]
        public void GetTopLimitErrorTest()
        {
            var top = AccountService.GetTop(1000, null);

            Assert.IsFalse(top.Success);
            Assert.IsNotNull(top.Error);
        }
    }
}