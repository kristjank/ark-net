using ArkNet.Utils;
using ArkNet.Utils.Enum;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ArkNet.Tests
{
    [TestClass()]
    public class ArkNetApiTests
    {
        private ArkNetApi _arkNetApi;
        public ArkNetApi ArkNetApi
        {
            get { return _arkNetApi ?? (_arkNetApi = new ArkNetApi()); }
        }

        [TestMethod()]
        public void StartTest()
        {
            ArkNetApi.Start(NetworkType.MainNet).Wait();

            Assert.IsNotNull(ArkNetApi.NetworkApi.NetworkSettings);
        }

        [TestMethod()]
        public async Task SwitchNetwork()
        {
            await ArkNetApi.Start(NetworkType.DevNet);
            await ArkNetApi.SwitchNetwork(NetworkType.MainNet);
            await ArkNetApi.SwitchNetwork(NetworkType.DevNet);
        }
    }
}