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
    public class DelegateServiceTests
    {
        [TestMethod()]
        public void GetAllTest()
        {
            var delegates = DelegateService.GetAll();

            Assert.IsNotNull(delegates);
        }

        [TestMethod()]
        public void GetByUsernameTest()
        {

            var dele = DelegateService.GetByUsername("arkpool");
            Assert.AreEqual(dele.Address, "ARAq9nhjCxwpWnGKDgxveAJSijNG8Y6dFQ");
        }

        [TestMethod()]
        public void GetByPubKeyTest()
        {
            var dele = DelegateService.GetByAddress("ARAq9nhjCxwpWnGKDgxveAJSijNG8Y6dFQ ");
            Assert.AreEqual("arkpool",dele.Username);

            var dele2 = DelegateService.GetByPubKey(dele.PublicKey);
            Assert.AreEqual(dele2.Username,dele.Username);

        }

        [TestMethod()]
        public void GetByAddressTest()
        {
            var dele = DelegateService.GetByAddress("ARAq9nhjCxwpWnGKDgxveAJSijNG8Y6dFQ  ");
            Assert.AreEqual("arkpool", dele.Username);
        }

        [TestMethod()]
        public void GetVotersTest()
        {
            var dele = DelegateService.GetVoters("arkpool");

            Assert.IsNotNull(dele);
        }
    }
}