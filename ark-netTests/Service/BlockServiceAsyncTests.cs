using ArkNet;
using ArkNet.Service;
using ArkNet.Utils.Enum;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkNetTest.Service
{
    [TestClass()]
    public class BlockServiceAsyncTests
    {
        [TestInitialize]
        public async Task Init()
        {
            await ArkNetApi.Instance.Start(NetworkType.MainNet);
        }

        [TestMethod()]
        public async Task GetAllAsyncTest()
        {
            var blocks = await BlockService.GetAllAsync();

            Assert.IsNotNull(blocks);
            Assert.IsNotNull(blocks.Blocks);
            Assert.IsTrue(blocks.Success);
            Assert.IsNull(blocks.Error);
            Assert.IsTrue(blocks.Blocks.Count > 0);
        }

        [TestMethod()]
        public async Task GetByIdAsyncTest()
        {
            var blocks = await BlockService.GetAllAsync();
            Assert.IsNotNull(blocks);
            Assert.IsNotNull(blocks.Blocks);
            Assert.IsTrue(blocks.Success);
            Assert.IsNull(blocks.Error);

            var block = blocks.Blocks.FirstOrDefault();
            Assert.IsNotNull(block);

            var block1 = BlockService.GetById(block.Id);
            Assert.IsNotNull(block1);
            Assert.IsTrue(block1.Success);
            Assert.IsNull(block1.Error);
            Assert.IsNotNull(block1.Block);
        }

        [TestMethod()]
        public async Task GetByIdErrorAsyncTest()
        {
            var block = await BlockService.GetByIdAsync("ErrorId");

            Assert.IsNotNull(block);
            Assert.IsFalse(block.Success);
            Assert.IsNotNull(block.Error);
        }

        [TestMethod()]
        public async Task GetEpochAsyncTest()
        {
            var epoch = await BlockService.GetEpochAsync();
            Assert.AreNotEqual(DateTime.MinValue, epoch);
            Assert.AreEqual(636256980000000000, epoch.Ticks);
        }

        [TestMethod()]
        public async Task GetHeightAsyncTest()
        {
            var height = await BlockService.GetHeightAsync();
            Assert.AreNotEqual(0, height);
        }

        [TestMethod()]
        public async Task GetNetHashAsyncTest()
        {
            var netHash = await BlockService.GetNetHashAsync();
            Assert.IsNotNull(netHash);
        }

        [TestMethod()]
        public async Task GetFeesAsyncTest()
        {
            var fees = await BlockService.GetFeesAsync();
            Assert.IsNotNull(fees);
        }

        [TestMethod()]
        public async Task GetMilestone()
        {
            var milestone = await BlockService.GetMilestoneAsync();
            Assert.IsNotNull(milestone);
        }

        [TestMethod()]
        public async Task GetReward()
        {
            var reward = await BlockService.GetRewardAsync();
            Assert.AreNotEqual(0, reward);
        }

        [TestMethod()]
        public async Task GetStatus()
        {
            var status = await BlockService.GetStatusAsync();

            Assert.IsNotNull(status);
            Assert.IsTrue(status.Success);
            Assert.IsNull(status.Error);
        }
    }
}
