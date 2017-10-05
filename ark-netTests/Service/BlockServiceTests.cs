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
    public class BlockServiceTests
    {
        [TestInitialize]
        public void Init()
        {
            ArkNetApi.Instance.Start(NetworkType.MainNet);
        }

        [TestMethod()]
        public void GetAllTest()
        {
            var blocks = BlockService.GetAll();
            Assert.IsTrue(blocks.Blocks.Count > 0);
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            var block = BlockService.GetAll().Blocks.FirstOrDefault();
            Assert.IsNotNull(block);

            var block1 = BlockService.GetById(block.Id);
            Assert.IsNotNull(block1.Block);
        }

        [TestMethod()]
        public void GetByIdErrorTest()
        {
            var block = BlockService.GetById("ErrorId");

            Assert.IsFalse(block.Success);
            Assert.IsNotNull(block.Error);
        }

        [TestMethod()]
        public void GetEpoch()
        {
            var epoch = BlockService.GetEpoch();
            Assert.IsNotNull(epoch);
        }

        [TestMethod()]
        public void GetHeight()
        {
            var height = BlockService.GetHeight();
            Assert.IsNotNull(height);
        }

        [TestMethod()]
        public void GetNetHash()
        {
            var netHash = BlockService.GetNetHash();
            Assert.IsNotNull(netHash);
        }

        [TestMethod()]
        public void GetFees()
        {
            var fees = BlockService.GetFees();
            Assert.IsNotNull(fees);
        }

        [TestMethod()]
        public void GetMilestone()
        {
            var milestone = BlockService.GetMilestone();
            Assert.IsNotNull(milestone);
        }

        [TestMethod()]
        public void GetReward()
        {
            var reward = BlockService.GetReward();
            Assert.IsNotNull(reward);
        }

        [TestMethod()]
        public void GetStatus()
        {
            var status = BlockService.GetStatus();
            Assert.IsNotNull(status);
        }
    }
}
