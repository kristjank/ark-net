using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArkNet.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArkNet.Utils.Enum;
using ArkNet.Messages.Transaction;

namespace ArkNet.Service.Transaction.Tests
{
    [TestClass()]
    public class TransactionServiceTests : TransactionServiceTestsBase
    {
        [TestInitialize]
        public void Init()
        {
            base.InitializeTransactionServiceTest();
        }

        [TestMethod()]
        public void GetAllTest()
        {
            var trans = ArkNetApi.TransactionService.GetAll();

            GetAllResultTest(trans);
        }

        [TestMethod()]
        public void GetGetTransactionsTest()
        {
            var trans = ArkNetApi.TransactionService.GetTransactions(new ArkTransactionRequest { BlockId = base._blockId });

            GetTransactionsResultTest(trans);
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            var trans = ArkNetApi.TransactionService.GetAll().Transactions.FirstOrDefault();
            Assert.IsNotNull(trans);

            var trans1 = ArkNetApi.TransactionService.GetById(trans.Id);

            GetByIdResultTest(trans1);
        }

        [TestMethod()]
        public void GetByIdErrorTest()
        {
            var trans = ArkNetApi.TransactionService.GetById("ErrorId");

            GetByIdErrorResultTest(trans);
        }

        [TestMethod()]
        public void GetUnConfirmedByIdTest()
        {
            var trans = ArkNetApi.TransactionService.GetUnconfirmedAll();
            Assert.IsNotNull(trans);

            if (trans.Transactions.Any())
            {
                var trans1 = ArkNetApi.TransactionService.GetUnConfirmedById(trans.Transactions.FirstOrDefault().Id);
                GetUnConfirmedByIdResultTest(trans1);
            }
        }

        [TestMethod()]
        public void GetUnConfirmedByIdErrorTest()
        {
            var trans = ArkNetApi.TransactionService.GetUnConfirmedById("ErrorId");

            GetUnConfirmedByIdErrorResultTest(trans);
        }

        [TestMethod()]
        public void GetUnconfirmedAllTest()
        {
            var trans = ArkNetApi.TransactionService.GetUnconfirmedAll();

            GetUnconfirmedAllResultTest(trans);
        }

        [TestMethod()]
        public void GetUnconfirmedTransactionsTest()
        {
            var trans = ArkNetApi.TransactionService.GetUnconfirmedTransactions(new ArkUnconfirmedTransactionRequest());

            GetUnconfirmedAllResultTest(trans);
        }
    }
}