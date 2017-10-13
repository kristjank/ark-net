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
using ArkNet.Model.Loader;
using ArkNet.Tests;

namespace ArkNet.Service.Loader.Tests
{
    public class LoaderServiceTestsBase : TestsBase
    {
        public void GetStatusResultTest(ArkLoaderStatus status)
        { 
            Assert.IsNotNull(status);
            Assert.IsTrue(status.Success);
            Assert.IsNull(status.Error);
        }

        public void GetSyncStatusResultTest(ArkLoaderStatusSync syncStatus)
        {
            Assert.IsNotNull(syncStatus);
            Assert.IsTrue(syncStatus.Success);
            Assert.IsNull(syncStatus.Error);
        }

        public void GetAutoConfigureParametersResultTest(ArkLoaderNetworkResponse parameters)
        {
            Assert.IsNotNull(parameters);
            Assert.IsNotNull(parameters.Network);
            Assert.IsTrue(parameters.Success);
            Assert.IsNull(parameters.Error);
        }
    }
}