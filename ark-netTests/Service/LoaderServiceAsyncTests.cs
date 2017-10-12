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
    public class LoaderServiceAsyncTests
    {
        [TestInitialize]
        public async Task Init()
        {
            await ArkNetApi.Instance.Start(NetworkType.MainNet);
        }

        [TestMethod()]
        public async Task GetStatusAsyncTest()
        {
            var status = await LoaderService.GetStatusAsync();

            Assert.IsNotNull(status);
            Assert.IsTrue(status.Success);
            Assert.IsNull(status.Error);
        }

        [TestMethod()]
        public async Task GetSyncStatusAsyncTest()
        {
            var syncStatus = await LoaderService.GetSyncStatusAsync();

            Assert.IsNotNull(syncStatus);
            Assert.IsTrue(syncStatus.Success);
            Assert.IsNull(syncStatus.Error);
        }

        [TestMethod()]
        public async Task GetAutoConfigureParametersAsyncTest()
        {
            var parameters = await LoaderService.GetAutoConfigureParametersAsync();

            Assert.IsNotNull(parameters);
            Assert.IsNotNull(parameters.Network);
            Assert.IsTrue(parameters.Success);
            Assert.IsNull(parameters.Error);
        }
    }
}