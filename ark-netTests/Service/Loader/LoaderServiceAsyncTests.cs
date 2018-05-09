using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArkNet.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArkNet.Utils.Enum;

namespace ArkNet.Service.Loader.Tests
{
    [TestClass()]
    public class LoaderServiceAsyncTests : LoaderServiceTestsBase
    {
        [TestInitialize]
        public async Task Init()
        {
            await base.InitializeAsync();
        }

        [TestMethod()]
        public async Task GetStatusAsyncTest()
        {
            var status = await ArkNetApi.LoaderService.GetStatusAsync();

            GetStatusResultTest(status);
        }

        [TestMethod()]
        public async Task GetSyncStatusAsyncTest()
        {
            var syncStatus = await ArkNetApi.LoaderService.GetSyncStatusAsync();

            GetSyncStatusResultTest(syncStatus);
        }

        [TestMethod()]
        public async Task GetAutoConfigureParametersAsyncTest()
        {
            var parameters = await ArkNetApi.LoaderService.GetAutoConfigureParametersAsync();

            GetAutoConfigureParametersResultTest(parameters);
        }
    }
}