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
using ArkNet.Model.Loader;
using ArkNet.Model.Peer;
using ArkNet.Model.Transactions;
using ArkNet.Tests;

namespace ArkNet.Service.Transaction.Tests
{
    public class TransactionServiceTestsBase : TestsBase
    {
        protected string _blockId = "1000003740392906241";

        public void InitializeTransactionServiceTest()
        {
            base.Initialize();

            Setup();
        }

        public async Task InitializeTransactionServiceAsyncTest()
        {
            await base.InitializeAsync();

            Setup();
        }

        private void Setup()
        {
            if (base.USE_DEV_NET)
            {
                _blockId = "13149578060728881902";
            }
        }

        public void GetAllResultTest(ArkTransactionList trans)
        {
            Assert.IsNotNull(trans);
            Assert.IsNotNull(trans.Transactions);
            Assert.IsTrue(trans.Success);
            Assert.IsNull(trans.Error);
            Assert.IsTrue(trans.Transactions.Count > 0);
        }

        public void GetTransactionsResultTest(ArkTransactionList trans)
        {
            Assert.IsNotNull(trans);
            Assert.IsNotNull(trans.Transactions);
            Assert.IsTrue(trans.Success);
            Assert.IsNull(trans.Error);
            if(base.USE_DEV_NET)
                Assert.IsTrue(trans.Transactions.Count == 50);
            else
                Assert.IsTrue(trans.Transactions.Count == 1);
        }

        public void GetByIdResultTest(ArkTransactionResponse trans1)
        {
            Assert.IsNotNull(trans1);
        }

        public void GetByIdErrorResultTest(ArkTransactionResponse trans)
        {
            Assert.IsNotNull(trans);
            Assert.IsNull(trans.Transaction);
            Assert.IsFalse(trans.Success);
            Assert.IsNotNull(trans.Error);
        }

        public void GetUnConfirmedByIdResultTest(ArkTransactionResponse trans1)
        {
            Assert.IsNotNull(trans1);
        }

        public void GetUnConfirmedByIdErrorResultTest(ArkTransactionResponse trans)
        {
            Assert.IsNotNull(trans);
            Assert.IsNull(trans.Transaction);
            Assert.IsFalse(trans.Success);
            Assert.IsNotNull(trans.Error);
        }

        public void GetUnconfirmedAllResultTest(ArkTransactionList trans)
        {
            Assert.IsNotNull(trans);
        }
    }
}