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
    public class BlockServiceAsyncTests : BlockServiceTestsBase
    {
        [TestInitialize]
        public async Task Init()
        {
            await base.InitializeBlockServiceAsyncTest();
        }

        [TestMethod()]
        public async Task GetAllAsyncTest()
        {
            var blocks = await ArkNetApi.BlockService.GetAllAsync();

            GetAllResultTest(blocks);
        }

        [TestMethod()]
        public void GetAllBlocksAsyncTest()
        {
            var blocks = ArkNetApi.BlockService.GetBlocks(new ArkBlockRequest { Height = base._height, GeneratorPublickey = base._generatorPublicKey });

            GetBlocksResultTest(blocks);
        }

        [TestMethod()]
        public async Task GetByIdAsyncTest()
        {
            var blocks = await ArkNetApi.BlockService.GetAllAsync();
            Assert.IsNotNull(blocks);
            Assert.IsNotNull(blocks.Blocks);
            Assert.IsTrue(blocks.Success);
            Assert.IsNull(blocks.Error);

            var block = blocks.Blocks.FirstOrDefault();
            Assert.IsNotNull(block);

            var block1 = ArkNetApi.BlockService.GetById(block.Id);
            GetByIdResultTest(block1);
        }

        [TestMethod()]
        public async Task GetByIdErrorAsyncTest()
        {
            var block = await ArkNetApi.BlockService.GetByIdAsync("ErrorId");

            GetByIdErrorResultTest(block);
        }

        [TestMethod()]
        public async Task GetEpochAsyncTest()
        {
            var epoch = await ArkNetApi.BlockService.GetEpochAsync();

            GetEpochResultTest(epoch);
        }

        [TestMethod()]
        public async Task GetHeightAsyncTest()
        {
            var height = await ArkNetApi.BlockService.GetHeightAsync();

            GetHeightResultTest(height);
        }

        [TestMethod()]
        public async Task GetNetHashAsyncTest()
        {
            var netHash = await ArkNetApi.BlockService.GetNetHashAsync();

            GetNetHashResultTest(netHash);
        }

        [TestMethod()]
        public async Task GetFeesAsyncTest()
        {
            var fees = await ArkNetApi.BlockService.GetFeesAsync();

            GetFeesResultTest(fees);
        }

        [TestMethod()]
        public async Task GetMilestone()
        {
            var milestone = await ArkNetApi.BlockService.GetMilestoneAsync();

            GetMilestoneResultTest(milestone);
        }

        [TestMethod()]
        public async Task GetReward()
        {
            var reward = await ArkNetApi.BlockService.GetRewardAsync();

            GetRewardResultTest(reward);
        }

        [TestMethod()]
        public async Task GetStatus()
        {
            var status = await ArkNetApi.BlockService.GetStatusAsync();

            GetStatusResultTest(status);
        }
    }
}
