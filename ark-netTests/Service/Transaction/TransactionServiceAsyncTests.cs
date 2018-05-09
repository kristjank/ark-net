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
    public class TransactionServiceAsyncTests : TransactionServiceTestsBase
    {
        [TestInitialize]
        public async Task Init()
        {
            await base.InitializeTransactionServiceAsyncTest();
        }

        [TestMethod()]
        public async Task GetAllAsyncTest()
        {
            var trans = await ArkNetApi.TransactionService.GetAllAsync();

            GetAllResultTest(trans);
        }

        [TestMethod()]
        public async Task GetGetTransactionsAsyncTest()
        {
            var trans = await ArkNetApi.TransactionService.GetTransactionsAsync(new ArkTransactionRequest { BlockId = base._blockId });

            GetTransactionsResultTest(trans);
        }

        [TestMethod()]
        public async Task GetByIdAsyncTest()
        {
            var transactions = await ArkNetApi.TransactionService.GetAllAsync();
            var transaction = transactions.Transactions.FirstOrDefault();
            Assert.IsNotNull(transaction);

            var trans1 = ArkNetApi.TransactionService.GetById(transaction.Id);

            GetByIdResultTest(trans1);
        }

        [TestMethod()]
        public async Task GetByIdErrorAsyncTest()
        {
            var trans = await ArkNetApi.TransactionService.GetByIdAsync("ErrorId");

            GetByIdErrorResultTest(trans);
        }

        [TestMethod()]
        public async Task GetUnConfirmedByIdAsyncTest()
        {
            var trans = await ArkNetApi.TransactionService.GetUnconfirmedAllAsync();
            Assert.IsNotNull(trans);

            if (trans.Transactions.Any())
            {
                var trans1 = await ArkNetApi.TransactionService.GetUnConfirmedByIdAsync(trans.Transactions.FirstOrDefault().Id);
                GetUnConfirmedByIdResultTest(trans1);
            }
        }

        [TestMethod()]
        public async Task GetUnConfirmedByIdErrorAsyncTest()
        {
            var trans = await ArkNetApi.TransactionService.GetUnConfirmedByIdAsync("ErrorId");

            GetUnConfirmedByIdErrorResultTest(trans);
        }

        [TestMethod()]
        public async Task GetUnconfirmedAllAsyncTest()
        {
            var trans = await ArkNetApi.TransactionService.GetUnconfirmedAllAsync();

            GetUnconfirmedAllResultTest(trans);
        }

        [TestMethod()]
        public async Task GetUnconfirmedTransactionsAsyncTest()
        {
            var trans = await ArkNetApi.TransactionService.GetUnconfirmedTransactionsAsync(new ArkUnconfirmedTransactionRequest());

            GetUnconfirmedAllResultTest(trans);
        }
    }
}