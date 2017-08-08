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
            Assert.IsNotNull(blocks);
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            var block = BlockService.GetAll().FirstOrDefault();
            Assert.IsNotNull(block);

            var block1 = BlockService.GetById(block.Id);
            Assert.IsNotNull(block1);
        }
    }
}
