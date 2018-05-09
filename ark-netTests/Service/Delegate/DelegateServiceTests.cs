using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArkNet.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArkNet.Utils.Enum;
using ArkNet.Messages.BaseMessages;

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
            var delegates = ArkNetApi.DelegateService.GetAll();

            GetAllResultTest(delegates);
        }

        [TestMethod()]
        public void GetDelegatesTest()
        {
            var delegates = ArkNetApi.DelegateService.GetDelegates(new ArkBaseRequest { OrderBy = "vote" });

            GetAllResultTest(delegates);
        }

        [TestMethod()]
        public void GetByUsernameTest()
        {
            var dele = ArkNetApi.DelegateService.GetByUsername(_userName);

            GetByUsernameResultTest(dele);
        }

        [TestMethod()]
        public void GetByUsernameErrorTest()
        {
            var dele = ArkNetApi.DelegateService.GetByUsername("NonExistingPool");

            GetByUsernameErrorResultTest(dele);
        }

        [TestMethod()]
        public void GetByPubKeyTest()
        {
            var dele2 = ArkNetApi.DelegateService.GetByPubKey(_pubKey);

            GetByPubKeyResultTest(dele2);
        }

        [TestMethod()]
        public void GetByPubKeyErrorTest()
        {
            var dele = ArkNetApi.DelegateService.GetByPubKey("ErrorKey");

            GetByPubKeyErrorResultTest(dele);
        }

        [TestMethod()]
        public void GetVotersTest()
        {
            var dele = ArkNetApi.DelegateService.GetVoters(_pubKey);

            GetVotersResultTest(dele);
        }

        [TestMethod()]
        public void GetVotersErrorTest()
        {
            var dele = ArkNetApi.DelegateService.GetVoters("ErrorKey");

            GetVotersErrorResultTest(dele);
        }

        [TestMethod()]
        public void GetFeeTest()
        {
            var fee = ArkNetApi.DelegateService.GetFee();

            GetFeeResultTest(fee);
        }

        [TestMethod()]
        public void GetForgedByAccountTest()
        {
            var forgedByAccount = ArkNetApi.DelegateService.GetForgedByAccount(_pubKey);

            GetForgedByAccountResultTest(forgedByAccount);
        }

        [TestMethod()]
        public void GetForgedByAccountErrorTest()
        {
            var forgedByAccount = ArkNetApi.DelegateService.GetForgedByAccount("ErrorKey");

            GetForgedByAccountErrorResultTest(forgedByAccount);
        }

        [TestMethod()]
        public void GetNextForgersTest()
        {
            var nextForgers = ArkNetApi.DelegateService.GetNextForgers();

            GetNextForgersResultTest(nextForgers);
        }

        [TestMethod()]
        public void GetTotalVoteArkTest()
        {
            var totalVoteArk = ArkNetApi.DelegateService.GetTotalVoteArk(_pubKey);

            GetTotalVoteArkResultTest(totalVoteArk);
        }

        [TestMethod()]
        public void GetTotalVoteArkErrorTest()
        {
            var totalVoteArk = ArkNetApi.DelegateService.GetTotalVoteArk("ErrorKey");

            GetTotalVoteArkErrorResultTest(totalVoteArk);
        }
    }
}