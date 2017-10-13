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
using ArkNet.Tests;

namespace ArkNet.Service.Delegate.Tests
{
    public class DelegateServiceTestsBase : TestsBase
    {
        protected string _userName = "arkpool";
        protected string _resultUserNameFromPubKey = "cryptolanka123";
        protected string _pubKey = "022a777d6010beac8fd1092a19adacde592e9ff88b84a1106ad9bd6f32001a737a";
        protected string _address = "ARAq9nhjCxwpWnGKDgxveAJSijNG8Y6dFQ";

        public void InitializeDelegateServiceTest()
        {
            base.Initialize();

            Setup();
        }

        public async Task InitializeDelegateServiceAsyncTest()
        {
            await base.InitializeAsync();

            Setup();
        }

        private void Setup()
        {
            if (USE_DEV_NET)
            {
                _userName = "darkjarunik";
                _resultUserNameFromPubKey = "d_chris";
                _pubKey = "02bcfa0951a92e7876db1fb71996a853b57f996972ed059a950d910f7d541706c9";
                _address = "DBi2HdDY8TqMCD2aFLVomEF92gzeDmEHmR";
            }
        }

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
            Assert.AreEqual(dele.Delegate.Address, _address);
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
            Assert.AreEqual(dele2.Delegate.Username, _resultUserNameFromPubKey);
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