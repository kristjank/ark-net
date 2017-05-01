using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArkNet.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkNet.Service.Tests
{
    [TestClass()]
    public class AccountServiceTests
    {
        [TestMethod()]
        public void GetByAddressTest()
        {
            var account = AccountService.GetByAddress("AQLUKKKyKq5wZX7rCh4HJ4YFQ8bpTpPJgK");

            Assert.IsNotNull(account);
        }
    }
}