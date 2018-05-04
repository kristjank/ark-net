using ArkNet.Utils;
using ArkNet.Utils.Enum;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}