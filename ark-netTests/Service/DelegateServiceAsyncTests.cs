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
    public class DelegateServiceAsyncTests
    {
        [TestInitialize]
        public async Task Init()
        {
            await ArkNetApi.Instance.Start(NetworkType.MainNet);
        }

        [TestMethod()]
        public async Task GetAllAsyncTest()
        {
            var delegates = await DelegateService.GetAllAsync();

            Assert.IsNotNull(delegates);
            Assert.IsNotNull(delegates.Delegates);
            Assert.IsTrue(delegates.Success);
            Assert.IsNull(delegates.Error);
            Assert.IsTrue(delegates.Delegates.Count > 0);
        }

        [TestMethod()]
        public async Task GetByUsernameAsyncTest()
        {
            var dele = await DelegateService.GetByUsernameAsync("arkpool");

            Assert.IsNotNull(dele);
            Assert.IsNotNull(dele.Delegate);
            Assert.IsTrue(dele.Success);
            Assert.IsNull(dele.Error);
            Assert.AreEqual(dele.Delegate.Address, "ARAq9nhjCxwpWnGKDgxveAJSijNG8Y6dFQ");
        }

        [TestMethod()]
        public async Task GetByUsernameErrorAsyncTest()
        {
            var dele = await DelegateService.GetByUsernameAsync("NonExistingPool");

            Assert.IsNotNull(dele);
            Assert.IsNull(dele.Delegate);
            Assert.IsFalse(dele.Success);
            Assert.IsNotNull(dele.Error);
        }

        [TestMethod()]
        public async Task GetByPubKeyAsyncTest()
        {
            var dele2 = await DelegateService.GetByPubKeyAsync("022a777d6010beac8fd1092a19adacde592e9ff88b84a1106ad9bd6f32001a737a");

            Assert.IsNotNull(dele2);
            Assert.IsNotNull(dele2.Delegate);
            Assert.IsTrue(dele2.Success);
            Assert.IsNull(dele2.Error);
            Assert.AreEqual(dele2.Delegate.Username,"cryptolanka123");
        }

        [TestMethod()]
        public async Task GetByPubKeyErrorAsyncTest()
        {
            var dele = await DelegateService.GetByPubKeyAsync("ErrorKey");

            Assert.IsNotNull(dele);
            Assert.IsNull(dele.Delegate);
            Assert.IsFalse(dele.Success);
            Assert.IsNotNull(dele.Error);
        }

        [TestMethod()]
        public async Task GetVotersAsyncTest()
        {
            var dele = await DelegateService.GetVotersAsync("022a777d6010beac8fd1092a19adacde592e9ff88b84a1106ad9bd6f32001a737a");

            Assert.IsNotNull(dele);
            Assert.IsNotNull(dele.Accounts);
            Assert.IsTrue(dele.Success);
            Assert.IsNull(dele.Error);
            Assert.IsTrue(dele.Accounts.Count > 0);
        }

        [TestMethod()]
        public async Task GetVotersErrorAsyncTest()
        {
            var dele = await DelegateService.GetVotersAsync("ErrorKey");

            Assert.IsNotNull(dele);
            Assert.IsNull(dele.Accounts);
            Assert.IsFalse(dele.Success);
            Assert.IsNotNull(dele.Error);
        }

        [TestMethod()]
        public async Task GetFeeAsyncTest()
        {
            var fee = await DelegateService.GetFeeAsync();

            Assert.IsTrue(fee > 0);
        }

        [TestMethod()]
        public async Task GetForgedByAccountAsyncTest()
        {
            var forgedByAccount = await DelegateService.GetForgedByAccountAsync("022a777d6010beac8fd1092a19adacde592e9ff88b84a1106ad9bd6f32001a737a");

            Assert.IsNotNull(forgedByAccount);
            Assert.IsTrue(forgedByAccount.Success);
            Assert.IsNull(forgedByAccount.Error);
        }

        [TestMethod()]
        public async Task GetForgedByAccountErrorAsyncTest()
        {
            var forgedByAccount = await DelegateService.GetForgedByAccountAsync("ErrorKey");

            Assert.IsNotNull(forgedByAccount);
            Assert.IsFalse(forgedByAccount.Success);
            Assert.IsNotNull(forgedByAccount.Error);
        }

        [TestMethod()]
        public async Task GetNextForgersAsyncTest()
        {
            var nextForgers = await DelegateService.GetNextForgersAsync();

            Assert.IsNotNull(nextForgers);
            Assert.IsNotNull(nextForgers.Delegates);
        }

        [TestMethod()]
        public async Task GetTotalVoteArkAsyncTest()
        {
            var totalVoteArk = await DelegateService.GetTotalVoteArkAsync("022a777d6010beac8fd1092a19adacde592e9ff88b84a1106ad9bd6f32001a737a");

            Assert.IsNotNull(totalVoteArk);
        }

        [TestMethod()]
        public async Task GetTotalVoteArkErrorAsyncTest()
        {
            var totalVoteArk = await DelegateService.GetTotalVoteArkAsync("ErrorKey");

            Assert.AreEqual(0, totalVoteArk);
        }
    }
}