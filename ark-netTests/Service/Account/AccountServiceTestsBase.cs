using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArkNet.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArkNet.Utils.Enum;
using ArkNet.Model.Account;
using ArkNet.Model.Delegate;

namespace ArkNet.Service.Account.Tests
{
    public class AccountServiceTestsBase
    {
        public void GetByAddressResultTest(ArkAccountResponse account)
        {
            Assert.IsNotNull(account);
            Assert.IsNotNull(account.Account);
            Assert.IsTrue(account.Success);
            Assert.IsNull(account.Error);
            Assert.AreEqual("AQLUKKKyKq5wZX7rCh4HJ4YFQ8bpTpPJgK", account.Account.Address);
        }

        public void GetByAddressErrorResultTest(ArkAccountResponse account)
        {
            Assert.IsNotNull(account);
            Assert.IsNull(account.Account);
            Assert.IsFalse(account.Success);
            Assert.IsNotNull(account.Error);
        }

        public void GetBalanceResultTest(ArkAccountBalance balance)
        {
            Assert.IsNotNull(balance);
            Assert.IsTrue(balance.Success);
            Assert.IsNull(balance.Error);
        }

        public void GetBalanceErrorResultTest(ArkAccountBalance balance)
        {
            Assert.IsNotNull(balance);
            Assert.IsFalse(balance.Success);
            Assert.IsNotNull(balance.Error);
        }

        public void GetDelegatesResultTest(ArkDelegateList delegates)
        {
            Assert.IsNotNull(delegates);
            Assert.IsNotNull(delegates.Delegates);
            Assert.IsTrue(delegates.Success);
            Assert.IsNull(delegates.Error);
        }

        public void GetDelegatesErrorResultTest(ArkDelegateList delegates)
        {
            Assert.IsNotNull(delegates);
            Assert.IsNull(delegates.Delegates);
            Assert.IsFalse(delegates.Success);
            Assert.IsNotNull(delegates.Error);
        }

        public void GetTopResultTest(ArkAccountTopList top)
        {
            Assert.IsNotNull(top);
            Assert.IsNotNull(top.Accounts);
            Assert.IsTrue(top.Success);
            Assert.IsNull(top.Error);
            Assert.AreEqual(100, top.Accounts.Count);
        }

        public void GetTopLimitResultTest(ArkAccountTopList top)
        {
            Assert.IsNotNull(top);
            Assert.IsNotNull(top.Accounts);
            Assert.IsTrue(top.Success);
            Assert.IsNull(top.Error);
            Assert.AreEqual(10, top.Accounts.Count);
        }

        public void GetTopRecordsToSkipResultTest(ArkAccountTopList top)
        {
            Assert.IsNotNull(top);
            Assert.IsNotNull(top.Accounts);
            Assert.IsTrue(top.Success);
            Assert.IsNull(top.Error);
            Assert.AreEqual(100, top.Accounts.Count);
        }

        public void GetTopLimitAndRecordsToSkipResultTest(ArkAccountTopList top)
        {
            Assert.IsNotNull(top);
            Assert.IsNotNull(top.Accounts);
            Assert.IsTrue(top.Success);
            Assert.IsNull(top.Error);
            Assert.AreEqual(10, top.Accounts.Count);
        }

        public void GetTopLimitErrorResultTest(ArkAccountTopList top)
        {
            Assert.IsNotNull(top);
            Assert.IsFalse(top.Success);
            Assert.IsNotNull(top.Error);
        }
    }
}