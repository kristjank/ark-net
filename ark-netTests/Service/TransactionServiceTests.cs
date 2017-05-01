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
    public class TransactionServiceTests
    {
        [TestMethod()]
        public void GetAllTest()
        {
            var trans = TransactionService.GetAll();
            Assert.IsNotNull(trans);
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            var trans = TransactionService.GetAll().FirstOrDefault();
            Assert.IsNotNull(trans);

            var trans1 = TransactionService.GetById(trans.Id);
            Assert.IsNotNull(trans1);
        }

        [TestMethod()]
        public void GetUnConfirmedByIdTest()
        {
            var trans = TransactionService.GetUnconfirmedAll().FirstOrDefault();
            Assert.IsNotNull(trans);

            var trans1 = TransactionService.GetUnConfirmedById(trans.Id);
            Assert.IsNotNull(trans1);
        }

        [TestMethod()]
        public void GetUnconfirmedAllTest()
        {
            var trans = TransactionService.GetUnconfirmedAll();
            Assert.IsNotNull(trans);
        }
    }
}