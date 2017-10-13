using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArkNet.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArkNet.Utils.Enum;

namespace ArkNet.Service.Delegate.Tests
{
    [TestClass()]
    public class DelegateServiceTests : DelegateServiceTestsBase
    {
        [TestInitialize]
        public void Init()
        {
            ArkNetApi.Instance.Start(NetworkType.MainNet).Wait();
        }

        [TestMethod()]
        public void GetAllTest()
        {
            var delegates = DelegateService.GetAll();

            GetAllResultTest(delegates);
        }

        [TestMethod()]
        public void GetByUsernameTest()
        {
            var dele = DelegateService.GetByUsername("arkpool");

            GetByUsernameResultTest(dele);
        }

        [TestMethod()]
        public void GetByUsernameErrorTest()
        {
            var dele = DelegateService.GetByUsername("NonExistingPool");

            GetByUsernameErrorResultTest(dele);
        }

        [TestMethod()]
        public void GetByPubKeyTest()
        {
            var dele2 = DelegateService.GetByPubKey("022a777d6010beac8fd1092a19adacde592e9ff88b84a1106ad9bd6f32001a737a");

            GetByPubKeyResultTest(dele2);
        }

        [TestMethod()]
        public void GetByPubKeyErrorTest()
        {
            var dele = DelegateService.GetByPubKey("ErrorKey");

            GetByPubKeyErrorResultTest(dele);
        }

        [TestMethod()]
        public void GetVotersTest()
        {
            var dele = DelegateService.GetVoters("022a777d6010beac8fd1092a19adacde592e9ff88b84a1106ad9bd6f32001a737a");

            GetVotersResultTest(dele);
        }

        [TestMethod()]
        public void GetVotersErrorTest()
        {
            var dele = DelegateService.GetVoters("ErrorKey");

            GetVotersErrorResultTest(dele);
        }

        [TestMethod()]
        public void GetFeeTest()
        {
            var fee = DelegateService.GetFee();

            GetFeeResultTest(fee);
        }

        [TestMethod()]
        public void GetForgedByAccountTest()
        {
            var forgedByAccount = DelegateService.GetForgedByAccount("022a777d6010beac8fd1092a19adacde592e9ff88b84a1106ad9bd6f32001a737a");

            GetForgedByAccountResultTest(forgedByAccount);
        }

        [TestMethod()]
        public void GetForgedByAccountErrorTest()
        {
            var forgedByAccount = DelegateService.GetForgedByAccount("ErrorKey");

            GetForgedByAccountErrorResultTest(forgedByAccount);
        }

        [TestMethod()]
        public void GetNextForgersTest()
        {
            var nextForgers = DelegateService.GetNextForgers();

            GetNextForgersResultTest(nextForgers);
        }

        [TestMethod()]
        public void GetTotalVoteArkTest()
        {
            var totalVoteArk = DelegateService.GetTotalVoteArk("022a777d6010beac8fd1092a19adacde592e9ff88b84a1106ad9bd6f32001a737a");

            GetTotalVoteArkResultTest(totalVoteArk);
        }

        [TestMethod()]
        public void GetTotalVoteArkErrorTest()
        {
            var totalVoteArk = DelegateService.GetTotalVoteArk("ErrorKey");

            GetTotalVoteArkErrorResultTest(totalVoteArk);
        }
    }
}