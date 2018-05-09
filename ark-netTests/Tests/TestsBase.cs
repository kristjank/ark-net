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

namespace ArkNet.Tests
{
    public class TestsBase
    {
        protected readonly bool USE_DEV_NET = true;

        private ArkNetApi _arkNetApi;
        public ArkNetApi ArkNetApi
        {
            get { return _arkNetApi ?? (_arkNetApi = new ArkNetApi()); }
        }

        public void Initialize()
        {
            if (USE_DEV_NET)
                ArkNetApi.Start(NetworkType.DevNet).Wait();
            else
                ArkNetApi.Start(NetworkType.MainNet).Wait();
        }

        public async Task InitializeAsync()
        {
            if (USE_DEV_NET)
                await ArkNetApi.Start(NetworkType.DevNet);
            else
                await ArkNetApi.Start(NetworkType.MainNet);
        }
    }
}