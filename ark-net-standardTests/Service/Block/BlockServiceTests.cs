using ArkNet;
using ArkNet.Service;
using ArkNet.Utils.Enum;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkNet.Service.Block.Tests
{
    [TestClass()]
    public class BlockServiceTests : BlockServiceTestsBase
    {
        [TestInitialize]
        public void Init()
        {
            base.Initialize();
        }

        [TestMethod()]
        public void GetAllTest()
        {
            var blocks = BlockService.GetAll();

            GetAllResultTest(blocks);
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            var block = BlockService.GetAll().Blocks.FirstOrDefault();
            Assert.IsNotNull(block);

            var block1 = BlockService.GetById(block.Id);

            GetByIdResultTest(block1);
        }

        [TestMethod()]
        public void GetByIdErrorTest()
        {
            var block = BlockService.GetById("ErrorId");

            GetByIdErrorResultTest(block);
        }

        [TestMethod()]
        public void GetEpochTest()
        {
            var epoch = BlockService.GetEpoch();

            GetEpochResultTest(epoch);
        }

        [TestMethod()]
        public void GetHeightTest()
        {
            var height = BlockService.GetHeight();

            GetHeightResultTest(height);
        }

        [TestMethod()]
        public void GetNetHashTest()
        {
            var netHash = BlockService.GetNetHash();

            GetNetHashResultTest(netHash);
        }

        [TestMethod()]
        public void GetFeesTest()
        {
            var fees = BlockService.GetFees();

            GetFeesResultTest(fees);
        }

        [TestMethod()]
        public void GetMilestoneTest()
        {
            var milestone = BlockService.GetMilestone();

            GetMilestoneResultTest(milestone);
        }

        [TestMethod()]
        public void GetRewardTest()
        {
            var reward = BlockService.GetReward();

            GetRewardResultTest(reward);
        }

        [TestMethod()]
        public void GetStatusTest()
        {
            var status = BlockService.GetStatus();

            GetStatusResultTest(status);
        }
    }
}
