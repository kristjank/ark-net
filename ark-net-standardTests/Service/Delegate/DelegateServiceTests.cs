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
            base.InitializeDelegateServiceTest();
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
            var dele = DelegateService.GetByUsername(_userName);

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
            var dele2 = DelegateService.GetByPubKey(_pubKey);

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
            var dele = DelegateService.GetVoters(_pubKey);

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
            var forgedByAccount = DelegateService.GetForgedByAccount(_pubKey);

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
            var totalVoteArk = DelegateService.GetTotalVoteArk(_pubKey);

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