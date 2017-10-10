using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArkNet.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArkNet.Utils.Enum;

namespace ArkNet.Service.Tests
{
    [TestClass()]
    public class LoaderServiceTests
    {
        [TestInitialize]
        public void Init()
        {
            ArkNetApi.Instance.Start(NetworkType.MainNet).Wait();
        }

        [TestMethod()]
        public void GetStatusTest()
        {
            var status = LoaderService.GetStatus();

            Assert.IsNotNull(status);
        }

        [TestMethod()]
        public void GetSyncStatusTest()
        {
            var syncStatus = LoaderService.GetSyncStatus();

            Assert.IsNotNull(syncStatus);
        }

        [TestMethod()]
        public void GetAutoConfigureParametersTest()
        {
            var parameters = LoaderService.GetAutoConfigureParameters();

            Assert.IsNotNull(parameters.Network);
        }
    }
}