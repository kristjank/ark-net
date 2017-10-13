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
using ArkNet.Model.Block;
using ArkNet.Utils;

namespace ArkNet.Service.Delegate.Tests
{
    public class DelegateServiceTestsBase
    {
        public void GetAllResultTest(ArkDelegateList delegates)
        {
            Assert.IsNotNull(delegates);
            Assert.IsNotNull(delegates.Delegates);
            Assert.IsTrue(delegates.Success);
            Assert.IsNull(delegates.Error);
            Assert.IsTrue(delegates.Delegates.Count > 0);
        }

        public void GetByUsernameResultTest(ArkDelegateResponse dele)
        {
            Assert.IsNotNull(dele);
            Assert.IsNotNull(dele.Delegate);
            Assert.IsTrue(dele.Success);
            Assert.IsNull(dele.Error);
            Assert.AreEqual(dele.Delegate.Address, "ARAq9nhjCxwpWnGKDgxveAJSijNG8Y6dFQ");
        }

        public void GetByUsernameErrorResultTest(ArkDelegateResponse dele)
        {
            Assert.IsNotNull(dele);
            Assert.IsNull(dele.Delegate);
            Assert.IsFalse(dele.Success);
            Assert.IsNotNull(dele.Error);
        }

        public void GetByPubKeyResultTest(ArkDelegateResponse dele2)
        {
            Assert.IsNotNull(dele2);
            Assert.IsNotNull(dele2.Delegate);
            Assert.IsTrue(dele2.Success);
            Assert.IsNull(dele2.Error);
            Assert.AreEqual(dele2.Delegate.Username, "cryptolanka123");
        }

        public void GetByPubKeyErrorResultTest(ArkDelegateResponse dele)
        {
            Assert.IsNotNull(dele);
            Assert.IsNull(dele.Delegate);
            Assert.IsFalse(dele.Success);
            Assert.IsNotNull(dele.Error);
        }

        public void GetVotersResultTest(ArkDelegateVoterList dele)
        {
            Assert.IsNotNull(dele);
            Assert.IsNotNull(dele.Accounts);
            Assert.IsTrue(dele.Success);
            Assert.IsNull(dele.Error);
            Assert.IsTrue(dele.Accounts.Count > 0);
        }

        public void GetVotersErrorResultTest(ArkDelegateVoterList dele)
        {
            Assert.IsNotNull(dele);
            Assert.IsNull(dele.Accounts);
            Assert.IsFalse(dele.Success);
            Assert.IsNotNull(dele.Error);
        }

        public void GetFeeResultTest(long fee)
        {
            Assert.IsTrue(fee > 0);
        }

        public void GetForgedByAccountResultTest(ArkDelegateForgedBalance forgedByAccount)
        {
            Assert.IsNotNull(forgedByAccount);
            Assert.IsTrue(forgedByAccount.Success);
            Assert.IsNull(forgedByAccount.Error);
        }

        public void GetForgedByAccountErrorResultTest(ArkDelegateForgedBalance forgedByAccount)
        {
            Assert.IsNotNull(forgedByAccount);
            Assert.IsFalse(forgedByAccount.Success);
            Assert.IsNotNull(forgedByAccount.Error);
        }

        public void GetNextForgersResultTest(ArkDelegateNextForgers nextForgers)
        {
            Assert.IsNotNull(nextForgers);
            Assert.IsNotNull(nextForgers.Delegates);
        }

        public void GetTotalVoteArkResultTest(long totalVoteArk)
        {
            Assert.IsNotNull(totalVoteArk);
        }

        public void GetTotalVoteArkErrorResultTest(long totalVoteArk)
        {
            Assert.AreEqual(0, totalVoteArk);
        }
    }
}