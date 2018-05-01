using ArkNet.Utils;
using ArkNet.Utils.Enum;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ArkNet.Tests
{
    [TestClass()]
    public class ArkNetApiTests
    {
        [TestMethod()]
        public void StartTest()
        {
            ArkNetApi.Instance.Start(NetworkType.MainNet).Wait();

            Assert.IsNotNull(ArkNetApi.Instance.NetworkSettings);
        }

        [TestMethod()]
        public async Task SwitchNetwork()
        {
            await ArkNetApi.Instance.Start(NetworkType.DevNet);
            await ArkNetApi.Instance.SwitchNetwork(NetworkType.MainNet);
            await ArkNetApi.Instance.SwitchNetwork(NetworkType.DevNet);
        }
    }
}