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
        protected int _height = 2339637;
        protected string _generatorPublicKey = "027a9b5dc98c75902f871e889fb3076dd27b11e158a49e3915e0307ecd9781f51e";

        public void InitializeBlockServiceTest()
        {
            base.Initialize();

            Setup();
        }

        public async Task InitializeBlockServiceAsyncTest()
        {
            await base.InitializeAsync();

            Setup();
        }

        private void Setup()
        {
            if (base.USE_DEV_NET)
            {
                _height = 1608634;
                _generatorPublicKey = "02b7b740973db16cd9c6f0f6f2bc160d27cd2a855e172d887833141bec234eb80c";
            }
        }

        public void GetAllResultTest(ArkBlockList blocks)
        {
            Assert.IsNotNull(blocks);
            Assert.IsNotNull(blocks.Blocks);
            Assert.IsTrue(blocks.Success);
            Assert.IsNull(blocks.Error);
            Assert.IsTrue(blocks.Blocks.Count > 0);
        }

        public void GetBlocksResultTest(ArkBlockList blocks)
        {
            Assert.IsNotNull(blocks);
            Assert.IsNotNull(blocks.Blocks);
            Assert.IsTrue(blocks.Success);
            Assert.IsNull(blocks.Error);
            Assert.IsTrue(blocks.Blocks.Count == 1);
            Assert.IsTrue(blocks.Blocks.First().Height == _height);
            Assert.IsTrue(blocks.Blocks.First().GeneratorPublicKey == _generatorPublicKey);
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