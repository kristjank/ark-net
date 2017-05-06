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
            ArkNetApi.Instance.Start(NetworkType.MainNet);
        }

        [TestMethod()]
        public void GetByAddressTest()
        {
            var account = AccountService.GetByAddress("AQLUKKKyKq5wZX7rCh4HJ4YFQ8bpTpPJgK");

            Assert.IsNotNull(account);
        }

        [TestMethod()]
        public void GetBalanceTest()
        {
            var res = AccountService.GetBalance("AQLUKKKyKq5wZX7rCh4HJ4YFQ8bpTpPJgK");

            Assert.IsTrue(res.status);
        }
    }
}