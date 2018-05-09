using ArkNet;
using ArkNet.Messages.Block;
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
            base.InitializeBlockServiceTest();
        }

        [TestMethod()]
        public void GetAllTest()
        {
            var blocks = ArkNetApi.BlockService.GetAll();

            GetAllResultTest(blocks);
        }

        [TestMethod()]
        public void GetAllBlocksTest()
        {
            var blocks = ArkNetApi.BlockService.GetBlocks(new ArkBlockRequest { Height = base._height, GeneratorPublickey = base._generatorPublicKey });

            GetBlocksResultTest(blocks);
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            var block = ArkNetApi.BlockService.GetAll().Blocks.FirstOrDefault();
            Assert.IsNotNull(block);

            var block1 = ArkNetApi.BlockService.GetById(block.Id);

            GetByIdResultTest(block1);
        }

        [TestMethod()]
        public void GetByIdErrorTest()
        {
            var block = ArkNetApi.BlockService.GetById("ErrorId");

            GetByIdErrorResultTest(block);
        }

        [TestMethod()]
        public void GetEpochTest()
        {
            var epoch = ArkNetApi.BlockService.GetEpoch();

            GetEpochResultTest(epoch);
        }

        [TestMethod()]
        public void GetHeightTest()
        {
            var height = ArkNetApi.BlockService.GetHeight();

            GetHeightResultTest(height);
        }

        [TestMethod()]
        public void GetNetHashTest()
        {
            var netHash = ArkNetApi.BlockService.GetNetHash();

            GetNetHashResultTest(netHash);
        }

        [TestMethod()]
        public void GetFeesTest()
        {
            var fees = ArkNetApi.BlockService.GetFees();

            GetFeesResultTest(fees);
        }

        [TestMethod()]
        public void GetMilestoneTest()
        {
            var milestone = ArkNetApi.BlockService.GetMilestone();

            GetMilestoneResultTest(milestone);
        }

        [TestMethod()]
        public void GetRewardTest()
        {
            var reward = ArkNetApi.BlockService.GetReward();

            GetRewardResultTest(reward);
        }

        [TestMethod()]
        public void GetStatusTest()
        {
            var status = ArkNetApi.BlockService.GetStatus();

            GetStatusResultTest(status);
        }
    }
}
