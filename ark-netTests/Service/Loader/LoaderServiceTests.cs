﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class LoaderServiceTests : LoaderServiceTestsBase
    {
        [TestInitialize]
        public void Init()
        {
            base.Initialize();
        }

        [TestMethod()]
        public void GetStatusTest()
        {
            var status = ArkNetApi.LoaderService.GetStatus();

            GetStatusResultTest(status);
        }

        [TestMethod()]
        public void GetSyncStatusTest()
        {
            var syncStatus = ArkNetApi.LoaderService.GetSyncStatus();

            GetSyncStatusResultTest(syncStatus);
        }

        [TestMethod()]
        public void GetAutoConfigureParametersTest()
        {
            var parameters = ArkNetApi.LoaderService.GetAutoConfigureParameters();

            GetAutoConfigureParametersResultTest(parameters);
        }
    }
}