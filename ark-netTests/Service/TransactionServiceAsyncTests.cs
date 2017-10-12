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
    public class TransactionServiceAsyncTests
    {
        [TestInitialize]
        public async Task Init()
        {
            await ArkNetApi.Instance.Start(NetworkType.MainNet);
        }

        [TestMethod()]
        public async Task GetAllAsyncTest()
        {
            var trans = await TransactionService.GetAllAsync();

            Assert.IsNotNull(trans);
            Assert.IsNotNull(trans.Transactions);
            Assert.IsTrue(trans.Success);
            Assert.IsNull(trans.Error);
            Assert.IsTrue(trans.Transactions.Count > 0);
        }

        [TestMethod()]
        public async Task GetByIdAsyncTest()
        {
            var transactions = await TransactionService.GetAllAsync();
            var transaction = transactions.Transactions.FirstOrDefault();
            Assert.IsNotNull(transaction);

            var trans1 = TransactionService.GetById(transaction.Id);
            Assert.IsNotNull(trans1);
        }

        [TestMethod()]
        public async Task GetByIdErrorAsyncTest()
        {
            var trans = await TransactionService.GetByIdAsync("ErrorId");

            Assert.IsNotNull(trans);
            Assert.IsNull(trans.Transaction);
            Assert.IsFalse(trans.Success);
            Assert.IsNotNull(trans.Error);
        }

        [TestMethod()]
        public async Task GetUnConfirmedByIdAsyncTest()
        {
            var trans = await TransactionService.GetUnconfirmedAllAsync();
            Assert.IsNotNull(trans);

            if (trans.Transactions.Any())
            {
                var trans1 = await TransactionService.GetUnConfirmedByIdAsync(trans.Transactions.FirstOrDefault().Id);
                Assert.IsNotNull(trans1);
            }
        }

        [TestMethod()]
        public async Task GetUnConfirmedByIdErrorAsyncTest()
        {
            var trans = await TransactionService.GetUnConfirmedByIdAsync("ErrorId");

            Assert.IsNotNull(trans);
            Assert.IsNull(trans.Transaction);
            Assert.IsFalse(trans.Success);
            Assert.IsNotNull(trans.Error);
        }

        [TestMethod()]
        public async Task GetUnconfirmedAllAsyncTest()
        {
            var trans = await TransactionService.GetUnconfirmedAllAsync();
            Assert.IsNotNull(trans);
        }
    }
}