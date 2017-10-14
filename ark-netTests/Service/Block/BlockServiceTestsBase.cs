using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArkNet.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArkNet.Utils.Enum;
using ArkNet.Model.Account;
using ArkNet.Model.Delegate;
using ArkNet.Model.Block;
using ArkNet.Utils;
using ArkNet.Tests;

namespace ArkNet.Service.Block.Tests
{
    public class BlockServiceTestsBase : TestsBase
    {
        public void GetAllResultTest(ArkBlockList blocks)
        {
            Assert.IsNotNull(blocks);
            Assert.IsNotNull(blocks.Blocks);
            Assert.IsTrue(blocks.Success);
            Assert.IsNull(blocks.Error);
            Assert.IsTrue(blocks.Blocks.Count > 0);
        }

        public void GetByIdResultTest(ArkBlockResponse block)
        {
            Assert.IsNotNull(block);
            Assert.IsTrue(block.Success);
            Assert.IsNull(block.Error);
            Assert.IsNotNull(block.Block);
        }

        public void GetByIdErrorResultTest(ArkBlockResponse block)
        {
            Assert.IsNotNull(block);
            Assert.IsFalse(block.Success);
            Assert.IsNotNull(block.Error);
        }

        public void GetEpochResultTest(DateTime epoch)
        {
            Assert.AreNotEqual(DateTime.MinValue, epoch);
            Assert.AreEqual(636256980000000000, epoch.Ticks);
        }

        public void GetHeightResultTest(long height)
        {
            Assert.AreNotEqual(0, height);
        }

        public void GetNetHashResultTest(string netHash)
        {
            Assert.IsNotNull(netHash);
        }

        public void GetFeesResultTest(Fees fees)
        {
            Assert.IsNotNull(fees);
        }

        public void GetMilestoneResultTest(int milestone)
        {
            Assert.IsNotNull(milestone);
        }

        public void GetRewardResultTest(int reward)
        {
            Assert.AreNotEqual(0, reward);
        }

        public void GetStatusResultTest(ArkBlockChainStatus status)
        {
            Assert.IsNotNull(status);
            Assert.IsTrue(status.Success);
            Assert.IsNull(status.Error);
        }
    }
}